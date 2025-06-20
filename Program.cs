using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Lista_De_Tarefas.Models;

namespace Lista_De_Tarefas;

public class Program
{
    private static readonly string CaminhoPadrao = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Notas.json");

    
    static void Main()
    {
        
        List<Tarefa> tarefas = carregarNota();
            bool executando = true;
            while (executando)
            {
                try
                {
                    Console.Clear();
                    mostrarOpcoes01();

                    int escolha1 = int.Parse(Console.ReadLine());
                    {
                        switch (escolha1)
                        {
                            case 1:
                                if (tarefas.Count == 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    exibirTexto("Você não tem nenhuma nota criada!", 50);
                                    Thread.Sleep(2000);
                                    Console.ResetColor();
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    exibirTexto("Selecione uma nota: \n", 50);
                                    for (int i = 0; i < tarefas.Count; i++)
                                    {
                                        Thread.Sleep(250);
                                        Console.WriteLine($"{i + 1}. {tarefas[i].Titulo}");
                                    }

                                    int escolha2 = int.Parse(Console.ReadLine());
                                    if (escolha2 != null)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Titulo: " + tarefas[escolha2 - 1].Titulo + "\n");
                                        Console.WriteLine("Conteúdo da nota: \n" + tarefas[escolha2 - 1].Assunto);
                                        Console.WriteLine(tarefas[escolha2 - 1].Data.ToString("dd/MM/yyyy HH:mm \n"));
                                    }
                                    
                                    mostrarOpcoes02();
                                    int escolha3 = int.Parse(Console.ReadLine());

                                    switch (escolha3)
                                    {
                                        case 1:
                                            Console.Clear();
                                            mostrarOpcoes03();
                                            int escolha4 = int.Parse(Console.ReadLine());
                                            switch (escolha4)
                                            {
                                                case 1:
                                                    Console.Clear();
                                                    exibirTexto("Digite um novo titulo: \n", 50);
                                                    string NovoTitulo = Console.ReadLine();
                                                    tarefas[escolha2 - 1].Titulo = NovoTitulo;
                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    exibirTexto("Titulo editado com sucesso!\n", 50);
                                                    Console.ResetColor();
                                                    salvarNota(tarefas);
                                                    break;
                                                case 2:
                                                    Console.Clear();
                                                    Console.WriteLine("\nDigite o novo conteúdo da nota: \n");
                                                    StringBuilder novoAssunto = new StringBuilder();
                                                    while (true)
                                                    {
                                                        string novalinha = Console.ReadLine();
                                                        if (novalinha == ".S" || novalinha == ".s")
                                                        {
                                                            break;
                                                        }

                                                        novoAssunto.Append(novalinha + "\n");
                                                    }
                                                    tarefas[escolha2 - 1].Assunto = novoAssunto.ToString();
                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    exibirTexto("Assunto editado com sucesso!\n", 50);
                                                    Console.ResetColor();
                                                    salvarNota(tarefas);
                                                    break;
                                                case 3:
                                                    break;
                                                default:
                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    exibirTexto("Opção invalida!!", 50);
                                                    Thread.Sleep(2000);
                                                    Console.ResetColor();
                                                    break;
                                            }
                                            break;
                                        case 2:
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            exibirTexto("Nota excluída com sucesso!", 50);
                                            Console.ResetColor();
                                            tarefas.RemoveAt(escolha2 - 1);
                                            salvarNota(tarefas);
                                            break;
                                        case 3:
                                            break;
                                        default:
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            exibirTexto("Opção invalida!!", 50);
                                            Thread.Sleep(2000);
                                            Console.ResetColor();
                                            break;
                                    }

                                    Console.Clear();
                                }

                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("Digite o titulo da nota: \n");
                                string Titulo = Console.ReadLine();
                                Console.WriteLine("\nDigite o conteúdo da nota: \n");
                                StringBuilder Assunto = new StringBuilder();
                                while (true)
                                {
                                    string linha = Console.ReadLine();
                                    if (linha == ".S" || linha == ".s")
                                    {
                                        break;
                                    }

                                    Assunto.Append(linha + "\n");
                                }

                                tarefas.Add(new Tarefa(Titulo, Assunto.ToString()));
                                salvarNota(tarefas);
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                exibirTexto("Nota criada com sucesso!", 50);
                                Thread.Sleep(2000);
                                Console.ResetColor();
                                Console.Clear();
                                break;
                            case 3:
                                executando = false;
                                break;
                            default:
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                exibirTexto("Opção invalida!!", 50);
                                Thread.Sleep(2000);
                                Console.ResetColor();
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    exibirTexto("Opção Invalida!!", 50);
                    Thread.Sleep(2000);
                    Console.ResetColor();
                }
            }
    }
    public static void mostrarOpcoes01()
    {
        string[] opcoes = {"Ver notas", "Criar nota", "Sair"};
        exibirTexto("Bem-vindo ao seu bloco de notas! \n", 50);

        for (int i = 0; i < opcoes.Length; i++)
        {
            Thread.Sleep(250);
            Console.WriteLine($"{(i + 1)}. {opcoes[i]}");
        }
    
    }

    public static void mostrarOpcoes02()
    {
        
        exibirTexto("O que deseja fazer? \n", 50);
        
        string[] opcoes = {"Editar nota", "Excluir nota", "Voltar"};
        for (int i = 0; i < opcoes.Length; i++)
        {
            Thread.Sleep(250);
            Console.WriteLine($"{(i + 1)}. {opcoes[i]}");
        }
        
    }

    public static void mostrarOpcoes03()
    {
        exibirTexto("O que deseja editar? \n", 50);
        string[] opcoes = { "Titulo", "Assunto", "Voltar" };
        for (int i = 0; i < opcoes.Length; i++)
        {
            Thread.Sleep(250);
            Console.WriteLine($"{(i + 1)}. {opcoes[i]}");
        }
    }

    public static void exibirTexto(string texto, int atraso)
    {
        foreach (char letra in texto)
        {
            Console.Write(letra);
            Thread.Sleep(atraso);
        }
    }

    public static void salvarNota(List<Tarefa> Tarefas)
    {
        
        string tarefasSalvas = JsonSerializer.Serialize(Tarefas, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(CaminhoPadrao, tarefasSalvas);
        
    }

    public static List<Tarefa> carregarNota()
    {

        if (File.Exists(CaminhoPadrao))
        {
            string json = File.ReadAllText(CaminhoPadrao);
            return JsonSerializer.Deserialize<List<Tarefa>>(json) ?? new List<Tarefa>();
        }
        return new List<Tarefa>();
    }

}

