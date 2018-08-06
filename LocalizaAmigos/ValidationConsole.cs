using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaAmigos
{
    public static class ValidationConsole
    {
        public static double ValidaCordenada(string _campo, string _valor)
        {
            int tentativa = 1;
            bool validation = false;            
            double retorno = -1;
            ValidaCampoVazio(_campo, ref _valor);
            while (!validation && !(tentativa > 4))
            {
                validation = double.TryParse(_valor, out retorno);

                if (!validation || retorno < 0 )
                {
                    Console.WriteLine("Valor inválido. Insira um valor válido: ");
                    Console.Write("{0}: ", _campo);
                    _valor = Console.ReadLine();
                    tentativa++;
                }
                else
                {
                    return retorno;
                }                
            }
            VoltarMenuInicial(validation);
            return 0;
        }

        public static void ValidaCampoVazio(string _campo, ref string _valor)
        {
            int tentativa = 1;
            bool validation = false;
            while (!validation && !(tentativa > 4))
            {
                if (_valor.Equals(null) || _valor == "")
                {
                    Console.WriteLine("O campo não pode ser vazio");
                    Console.Write("{0}: ", _campo);
                    _valor = Console.ReadLine();
                    validation = false;
                }
                else
                {
                    validation = true;
                }
                tentativa++;
            }
            VoltarMenuInicial(validation);            
        }
        static void VoltarMenuInicial(bool _voltar)
        {
            if (!_voltar)
            {
                Console.Write("Você excedeu a quantidade de tentativas, aperte enter para voltar ao Menu Inicial");
                Console.ReadLine();
                Menu menu = new Menu();
            }
        }
    }
}
