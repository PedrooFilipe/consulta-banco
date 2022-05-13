using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consulta_banco
{
    public class UpdateJob : IJob
    {
        IUsuarioRepository iUsuarioRepository;
        public UpdateJob(IUsuarioRepository iUsuarioRepository)
        {
            this.iUsuarioRepository = iUsuarioRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run (() => test());
            return task;
        }

        public async Task test()
        {
            Console.WriteLine("chegou");
            try
            {
                await iUsuarioRepository.UpdateAll();
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
