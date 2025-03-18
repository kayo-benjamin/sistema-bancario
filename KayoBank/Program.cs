using System;
using System.Collections.Generic;
using System.Linq;

namespace KayoBank
{
    //Tipos de Contas
    public enum TipoConta
    {
        Agua,
        Energia,
        Internet,
        Aluguel,
        Supermercado,
        Outros
    }

    public class Conta
    {
        private static int _proximoId = 1;
        public int ID { get; }
        public string Descricao { get; private set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; private set; }
        public bool Pago { get; private set; }
        public TipoConta Tipo { get; private set; }

        // Construtor da classe Conta
        public Conta(string descricao, double valor, DateTime dataVencimento, TipoConta tipo)
        {
            ID = _proximoId++;
            Descricao = descricao;
            Valor = valor;
            DataVencimento = dataVencimento;
            Pago = false; // Conta começa como não paga
            Tipo = tipo;
        }
        //Marca a conta como paga
        public void RealizarPagamento()
        {
            if (!Pago)
            {
                Pago = true;
                Console.WriteLine("Pagamento realizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Essa conta já foi paga!");
            }
        }
        //Verifica se a conta esta atrasada
        public bool EstahAtrasado()
        {
            return !Pago && DateTime.Now > DataVencimento;
        }

        //Exibe os detalhes da conta
        public void ExibirDetalhes()
        {
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Descrição: {Descricao}");
            Console.WriteLine($"Valor: {Valor:C}");
            Console.WriteLine($"Data de Vencimento: {DataVencimento:dd/MM/yyyy}");
            Console.WriteLine($"Status: {(Pago ? "Pago" : "Pendente")}");
            Console.WriteLine($"Tipo de Conta: {Tipo}\n");
        }

        //Usado ToString para exibir os dados de uma maneira mais legível
        public override string ToString()
        {
            return $"ID: {ID}\nDescrição: {Descricao}\nValor: {Valor:C}\n" +
                   $"Vencimento: {DataVencimento:dd/MM/yyyy}\nStatus: {(Pago ? "Pago" : "Pendente")}\n" +
                   $"Tipo: {Tipo}\n";
        }

    }
    //Gerenciamento das Contas
    public class GerenciadorContas
    {
        private List<Conta> contas = new List<Conta>();

        //Adiciona uma nova conta
        public void AdicionarConta(Conta conta)
        {
            contas.Add(conta);
            Console.WriteLine("Conta adicionada com sucesso!");
            LimparTela();
        }
        //Remove a conta pelo ID
        public void RemoverConta(int id)
        {
            var conta = EncontrarConta(id);
            if (conta != null)
            {
                contas.Remove(conta);
                Console.WriteLine("Conta removida com sucesso!");
                LimparTela();
            }
            else
            {
                Console.WriteLine("Conta não encontrada!");
                LimparTela();
            }
        }
        //Encontra a conta pelo ID
        public Conta EncontrarConta(int id)
        {
            return contas.FirstOrDefault(c => c.ID == id);
        }
        //Paga a conta
        public void PagarConta(int id)
        {
            var conta = EncontrarConta(id);
            if (conta != null)
            {
                conta.RealizarPagamento();
                LimparTela();
            }
            else
            Console.Clear();
            {
                Console.WriteLine("Conta não encontrada!");
                LimparTela();
            }
        }
        //Lista todas as contas
        public void ListarContas()
        {
            if (!contas.Any())
            {
                Console.Clear();
                Console.WriteLine("Nenhuma conta cadastrada!");
                LimparTela();
            }
            Console.WriteLine("Lista de Contas:");
            foreach (var conta in contas)
            {
                Console.Clear();
                Console.WriteLine(conta);
                LimparTela();
            }
        }
        //Lista as contas pendentes
        public void ContasPendentes()
        {
            var pendentes = contas.Where(c => !c.Pago).ToList();
            if (!pendentes.Any())
            {
                Console.Clear();
                Console.WriteLine("Nenhuma conta pendente!");
                LimparTela();
            }
            Console.WriteLine("Contas Pendentes:");
            foreach (var conta in pendentes)
            {
                Console.Clear();
                conta.ExibirDetalhes();
                LimparTela();
            }
        }
        //Lista as contas atrasadas
        public void ContasAtrasadas()
        {
            var atrasadas = contas.Where(c => c.EstahAtrasado()).ToList();
            Console.Clear();
            if (!atrasadas.Any())
            {
                Console.WriteLine("Nenhuma conta atrasada.");
                LimparTela();
                return;
            }
            Console.WriteLine("Contas Atrasadas:");
            foreach (var conta in atrasadas)
            {
                conta.ExibirDetalhes();
            }
            LimparTela();
        }
        //Mostra o total de contas pendentes
        public double TotalContasPendentes()
        {
            return contas.Where(c => !c.Pago).Sum(c => c.Valor);
        }
        //Mostra o total de contas pagas
        public double TotalContasPagas()
        {
            return contas.Where(c => c.Pago).Sum(c => c.Valor);
        }

        public void LimparTela()
        {
            Console.WriteLine("Clique em qualquer tela para o Menu Principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
    //Classe Principal
    class Program
    {
        //Método stático para usar na classe principal
        static GerenciadorContas gerenciador = new GerenciadorContas();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("--- Kayo's Bank ---");
                Console.WriteLine("1- Adicionar Conta");
                Console.WriteLine("2- Remover Conta");
                Console.WriteLine("3- Pagar Conta");
                Console.WriteLine("4- Todas as Contas");
                Console.WriteLine("5- Contas Pendentes");
                Console.WriteLine("6- Contas Atrasadas");
                Console.WriteLine("7- Calcular Pendências");
                Console.WriteLine("8- Calcular Contas Pagas");
                Console.WriteLine("0- Sair");
                Console.Write("Escolha uma opção: ");
                int op = Convert.ToInt32(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        AdicionarConta();
                        break;
                    case 2:
                        RemoverConta();
                        break;
                    case 3:
                        PagarConta();
                        break;
                    case 4:
                        gerenciador.ListarContas();
                        break;
                    case 5:
                        gerenciador.ContasPendentes();
                        break;
                    case 6:
                        gerenciador.ContasAtrasadas();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine($"Total pendente: {gerenciador.TotalContasPendentes():C}");
                        LimparTela();
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine($"Total pago: {gerenciador.TotalContasPagas():C}");
                        LimparTela();
                        break;
                    case 0:
                        Sair();
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida!");
                        LimparTela();
                        continue;
                }
            }
        }

        //Limpa a tela

        static void Sair()
        {
            Console.WriteLine("Saindo...");
            Thread.Sleep(2000);
        }

        static void LimparTela()
        {
            Console.WriteLine("Clique em qualquer tela para o Menu Principal");
            Console.ReadKey();
            Console.Clear();
        }
        //Adiciona a conta
        static void AdicionarConta()
        {
            Console.Clear();
            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Valor: ");
            if (!double.TryParse(Console.ReadLine(), out double valor))
            {
                Console.WriteLine("Valor inválido.");
                return;
            }

            Console.Write("Data de Vencimento (dd/MM/yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataVencimento))
            {
                Console.WriteLine("Data inválida.");
                return;
            }
            Console.WriteLine("Escolha o tipo de conta:");
            foreach (var tipo in Enum.GetValues(typeof(TipoConta)))
            {
                Console.WriteLine($"{(int)tipo} - {tipo}");
            }
            Console.Write("Opção: ");
            if (!int.TryParse(Console.ReadLine(), out int tipoEscolhido) || !Enum.IsDefined(typeof(TipoConta), tipoEscolhido))
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }

            TipoConta tipoConta = (TipoConta)tipoEscolhido;


            Conta novaConta = new Conta(descricao, valor, dataVencimento, tipoConta);

            Console.WriteLine("Criando conta...");
            Thread.Sleep(2000);
            gerenciador.AdicionarConta(novaConta);
        }

        //Remove a conta
        static void RemoverConta()
        {
            Console.Clear();
            Console.Write("\nID da conta a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                LimparTela();
            }

            Console.WriteLine("Removendo conta...");
            Thread.Sleep(2000);
            gerenciador.RemoverConta(id);
        }

        //Paga a conta
        static void PagarConta()
        {
            Console.Clear();
            Console.Write("\nID da conta a pagar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                LimparTela();
            }

            var conta = gerenciador.EncontrarConta(id);
            if (conta != null)
            {
                if (conta.Pago)
                {
                    Console.WriteLine("A conta já foi paga.");
                    LimparTela();
                }
                else
                {
                    conta.RealizarPagamento();
                    LimparTela();
                }
            }
            else
            {
                Console.WriteLine("Conta não encontrada!");
                LimparTela();
            }
        }
    }
}