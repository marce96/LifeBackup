using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xunit;

namespace LifeBackupIntegration.Tests.Setup
{
    public class TestContext : IAsyncLifetime
    {
        //IAsyncLifetime: Allow us to bring in an InitializeAsync and a disposeAsync method
        //InitializeAsync will run straight after the class's constructor.
        //Docker Egine is the underlying client server technology that builds and runs containers using Docker's
        //components and services
        private readonly DockerClient _dockerClient;
        private const string ContainerImageUri = "localstack/localstack";
        private string _containerId { get; set; }
        public TestContext()
        {
            _dockerClient = new DockerClientConfiguration(new Uri(DockerApiUri())).CreateClient();
        }
        public async Task InitializeAsync()
        {
            await PullImage();
            await StartContainer();
        }

        //LocalStack contains many local instaces of AWS services including S3.
        private async Task PullImage()
        {
            await _dockerClient.Images.CreateImageAsync(
                new ImagesCreateParameters
                {
                    FromImage = ContainerImageUri,
                    Tag = "latest"
                },
                new AuthConfig(), //username or password to access the docker image.  Is anonymous rn.
                new Progress<JSONMessage>() //checks up on the progress of pulling down the docker image            
                );
        }

        private async Task StartContainer()
        {
            var response = await _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = ContainerImageUri,
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    { "9003", default }
                },
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>{
                        { "9003", new List<PortBinding> {new PortBinding {HostPort = "9003" } } }
                    }
                },
                Env = new List<string> { "SERVICES=s3:9003"}
            });

            _containerId = response.ID;

            await _dockerClient.Containers.StartContainerAsync(_containerId, null);
        }

        private string DockerApiUri()
        {
            var isWindow = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindow)
                return "npipe://./pipe/docker_engine";

            var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            if (isLinux)
                return "unix:/var/run/docker.sock";

            throw new Exception("Unable to determine what OS this is running on.");
        }
        public async Task DisposeAsync()
        {
            if (_containerId != null)
                await _dockerClient.Containers.KillContainerAsync(_containerId, new ContainerKillParameters());
        }

    }
}
