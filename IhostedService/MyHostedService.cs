using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.IhostedService
{
    public class MyHostedService : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nameFile = "Archivo.txt";
        public MyHostedService(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            write("Inicio");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            write("Finalizo");

            return Task.CompletedTask;

        }

        private void write(string message)
        {
            string root = $@"{env.ContentRootPath}/wwwroot/{nameFile}";
            using (StreamWriter writer = new StreamWriter(root, true))
            {
                writer.WriteLine(message);
            }
        }
    }
}