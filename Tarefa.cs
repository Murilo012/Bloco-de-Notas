namespace Lista_De_Tarefas.Models;

public class Tarefa
{
    public string Titulo { get; set; }
    public string Assunto { get; set; }
    public DateTime Data { get; private set; }

    public Tarefa() {}
    public Tarefa(string titulo, string assunto)
    {
        this.Titulo = titulo;
        this.Assunto = assunto;
        this.Data = DateTime.Now;
    }
}