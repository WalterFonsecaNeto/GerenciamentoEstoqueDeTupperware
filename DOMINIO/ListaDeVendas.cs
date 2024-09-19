public class ListaDeVendas
{

    #region Atributos
    private DateTime _dataHora;
    private Produto _produtoVendido;
    private int _quantidadeVendida;
    #endregion


    #region Propiedades
    public DateTime DataHora
    {
        get { return _dataHora; }
        set { _dataHora = value; }
    }
    public Produto ProdutoVendido
    {
        get { return _produtoVendido; }
        set { _produtoVendido = value; }
    }

    public int QuantidadeVendida
    {
        get { return _quantidadeVendida; }
        set { _quantidadeVendida = value; }
    }
    #endregion


    #region Construtores
    public ListaDeVendas(Produto produto, int quantidadeVendida)
    {
        this._dataHora = DateTime.Now;
        this._produtoVendido = produto;
        this._quantidadeVendida = quantidadeVendida;


    }
    #endregion


}