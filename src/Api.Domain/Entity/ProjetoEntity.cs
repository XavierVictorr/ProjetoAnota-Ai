namespace Domain.Entity;

public class ProjetoEntity
{
    public string Nome { get; set; }
    public List<TarefaEntity> ListaTarefas { get; set; }
}