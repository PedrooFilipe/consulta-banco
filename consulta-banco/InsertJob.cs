using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consulta_banco
{
    public class InsertJob: IJob
    {
        IUsuarioRepository iUsuarioRepository;
        public InsertJob(IUsuarioRepository iUsuarioRepository)
        {
            this.iUsuarioRepository = iUsuarioRepository;
            Console.WriteLine("teste");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run (() => SaveAll());
            return task;
        }

        public async Task SaveAll()
        {
            Console.WriteLine("chegou");
            try
            {
                await iUsuarioRepository.SaveAll();
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
