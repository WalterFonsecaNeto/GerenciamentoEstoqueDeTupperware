public class Caixa
{
    #region Atributos
    public decimal _valorCaixa;

    #endregion==========================================================================



    #region Metodos
    public void AdicionarDinheiro(Produto produto, int quantidade)
    {
        _valorCaixa = _valorCaixa + (produto.Preco * quantidade);

    }
    public void RemoverDinheiro(Produto produto, int quantidade)
    {
        if (produto.Preco * quantidade <= _valorCaixa)
        {
            _valorCaixa = _valorCaixa - (produto.Preco * quantidade);
        }
        else
        {
            throw new Exception("VALOR DA DEVOLUÇÃO MAIOR QUE O VALOR DO CAIXA");
        }

    }

    #endregion==========================================================================







}