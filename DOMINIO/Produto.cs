public class Produto
{
    #region Atributos

    protected string _nome;
    protected string _cor;
    protected string _tamanho;
    protected decimal _preco;
    protected int _quantidade;

    #endregion==========================================================================


    #region Propiedades

    public string Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }
    public string Cor
    {
        get { return _cor; }
        set { _cor = value; }
    }
    public string Tamanho
    {
        get { return _tamanho; }
        set { _tamanho = value; }
    }
    public decimal Preco
    {
        get { return _preco; }
        set
        {
            if (value >= 0)
            {
                _preco = value;
            }
            else
            {
                throw new Exception("O VALOR DIGITADO E UM VALOR NEGATIVO, TENTE NOVAMENTE");
            }
        }
    }
    public int Quantidade
    {
        get { return _quantidade; }
        set
        {
            if (value >= 0)
            {
                _quantidade = value;
            }
            else
            {
                throw new Exception("O VALOR DIGITADO E UM VALOR NEGATIVO, TENTE NOVAMENTE");
            }
        }
    }

    #endregion==========================================================================

    #region Contrutores

    public Produto(string nome, string cor, string tamanho, decimal preco, int quantidade)
    {
        this._nome = nome;
        this._cor = cor;
        this._tamanho = tamanho;
        this._preco = preco;
        this._quantidade = quantidade;
    }
    #endregion


}