#region Bibliotecas
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Spectre.Console;
#endregion

public class Program
{
    #region Variaveis Globais
    public static string nome;
    public static string cor;
    public static string tamanho;
    public static decimal preco;
    public static int quantidade;
    public static string novoJson;
    public static string json;
    public static Caixa caixa = new Caixa();
    public static List<Produto> produtos = new List<Produto>();
    public static List<ListaDeVendas> listaDeVendas = new List<ListaDeVendas>();
    public static menuInicial opMInicial;
    public static menuCadastro opMCadastro;
    public static menuModificacao opMModificacao;
    public static menuRelatorio opMRelatorio;
    public static menuCaixa opMCaixa;

    #endregion


    #region Programa Principal
    static void Main(string[] args)
    {
        ProgramaGeral();
    }

    #endregion


    #region Funções do Menu Inicial
    public static void MenuInicial()
    {
        var table = new Table().Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold white]GERENCIAMENTO DE ESTOQUE DA TUPPERWARE[/]").Centered());
        table.AddRow("[bold white]1 - CADASTRO[/]                                    [bold white]2 - MODIFICAÇÃO[/]\n");
        table.AddRow("[bold white]3 - RELATORIO[/]                                         [bold white]4 - CAIXA[/]\n");
        table.AddRow("[bold white]0 - SAIR[/]").Centered();
        AnsiConsole.Write(table);
        AnsiConsole.Markup("[bold white]DIGITE SUA OPÇÃO --> [/]");
    }
    public static void ProgramaGeral()
    {
        try
        {
            VerificarProduto();
            VerificarListaDeVendas();
            VerificarCaixa();

            do
            {
                MenuInicial();
                opMInicial = Enum.Parse<menuInicial>(Console.ReadLine());
                Console.Clear();

                switch (opMInicial)
                {
                    case menuInicial.Cadastro:
                        ProgramaCadastro();
                        break;
                    case menuInicial.Modificação:
                        ProgramaModificacao();
                        break;
                    case menuInicial.Relatorio:
                        ProgramaRelatorio();
                        break;
                    case menuInicial.Caixa:
                        ProgramaCaixa();
                        break;
                    case menuInicial.Sair:
                        break;
                    default:
                        throw new Exception("Opção do menu inicial invalida, tente novamente.");


                }
            } while (opMInicial != menuInicial.Sair);
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup($"[bold red]OCORREU UM ERRO INESPERADO: {ex.Message}[/]\n");
            Console.ReadKey();
            Console.Clear();
            ProgramaGeral();
        }
    }
    #endregion==============================================================


    #region Funções do Menu Cadastro
    public static void MenuCadastro()
    {
        var table = new Table()
        .Expand(); // Aumenta o tamanho da tabela
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold white]MENU CADASTRO[/]").Centered());
        table.AddRow("[bold white]1 - CADASTRAR PRODUTO [/]                                   [bold white]0 - VOLTAR [/]").Centered();
        AnsiConsole.Write(table);
        AnsiConsole.Markup("[bold white]DIGITE SUA OPÇÃO --> [/]");
    }
    public static void ProgramaCadastro()
    {
        try
        {



            MenuCadastro();
            opMCadastro = Enum.Parse<menuCadastro>(Console.ReadLine());
            Console.Clear();

            switch (opMCadastro)
            {
                case menuCadastro.CadastrarProduto:
                    FuncaoCadastrarProduto();
                    break;

                case menuCadastro.Voltar:
                    FuncaoSairMenuCadastro();
                    break;

                default:
                    throw new Exception("Opção do menu cadastro invalida, tente novamente.");
            }

        }
        catch (Exception ex)
        {
            AnsiConsole.Markup($"[bold red]OCORREU UM ERRO INESPERADO: {ex.Message}[/]\n");
            Console.ReadKey();
            Console.Clear();
            ProgramaCadastro();
        }

    }
    static void FuncaoCadastrarProduto()
    {
        VerificarProduto();

        AnsiConsole.Markup("[bold white]DIGITE O NOME DO PRODUTO[/]\n");
        nome = Console.ReadLine();

        AnsiConsole.Markup("[bold white]DIGITE A COR DO PRODUTO[/]\n");
        cor = Console.ReadLine();

        AnsiConsole.Markup("[bold white]DIGITE O TAMANHO DO PRODUTO[/]\n");
        tamanho = Console.ReadLine();

        if (produtos.Any(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho))
        {
            throw new Exception("O PRODUTO JÁ EXISTE, TENTE NOVAMENTE\n");
        }
        else
        {
            AnsiConsole.Markup("[bold white]DIGITE O PREÇO DO PRODUTO[/]\n");
            preco = decimal.Parse(Console.ReadLine());

            AnsiConsole.Markup("[bold white]DIGITE QUANTIDADE DE PRODUTO[/]\n");
            quantidade = int.Parse(Console.ReadLine());

            Produto produto = new Produto(nome, cor, tamanho, preco, quantidade);
            produtos.Add(produto);

            SalvarProdutos();

            AnsiConsole.Markup("[bold green]PRODUTO CADASTRADO COM SUCESSO[/]\n");
        }


    }
    public static void FuncaoSairMenuCadastro()
    {
        opMCadastro = menuCadastro.Voltar;
    }
    #endregion==============================================================


    #region Funções do Menu Modificação
    public static void MenuModificacao()
    {
        var table = new Table().Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold white]MENU MODIFICAÇÕES[/]").Centered());
        table.AddRow("[bold white]1 - REMOVER PRODUTO[/]                                         [bold white]2 - MUDAR NOME[/]\n").Centered();
        table.AddRow("[bold white]3 - MUDAR COR[/]                                            [bold white]4 - MUDAR TAMANHO[/]\n").Centered();
        table.AddRow("[bold white]5 - MUDAR PREÇO[/]                                       [bold white]6 - MUDAR QUANTIDADE[/]\n").Centered();
        table.AddRow("[bold white]0 - VOLTAR[/] \n").Centered();
        AnsiConsole.Write(table);
        AnsiConsole.Markup("[bold white]DIGITE SUA OPÇÃO --> [/]");
    }
    public static void ProgramaModificacao()
    {
        try
        {
            do
            {
                MenuModificacao();
                opMModificacao = Enum.Parse<menuModificacao>(Console.ReadLine());
                Console.Clear();

                switch (opMModificacao)
                {
                    case menuModificacao.RemoverProduto:
                        FuncaoRemoverProduto();
                        break;
                    case menuModificacao.MudarNome:
                        FuncaoMudarNome();
                        break;
                    case menuModificacao.MudarCor:
                        FuncaoMudarCor();
                        break;
                    case menuModificacao.MudarTamanho:
                        FuncaoMudarTamanho();
                        break;
                    case menuModificacao.MudarPreco:
                        FuncaoMudarPreco();
                        break;
                    case menuModificacao.MudarQuantidade:
                        FuncaoMudarQuantidade();
                        break;
                    case menuModificacao.Voltar:
                        FuncaoSairMenuModificacao();
                        break;

                    default:
                        throw new Exception("Opção do menu cadastro invalida, tente novamente.");
                }
            } while (opMModificacao != menuModificacao.Voltar);
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup($"[bold red]OCORREU UM ERRO INESPERADO: {ex.Message}[/]\n");
            Console.ReadKey();
            Console.Clear();
            ProgramaModificacao();
        }

    }
    public static void FuncaoRemoverProduto()
    {
        VerificarProduto();
        LerProdutos();

        AnsiConsole.Markup("[bold white]DIGITE O NOME DO PRODUTO[/]\n");
        nome = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE A COR DO PRODUTO[/]\n");
        cor = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE O TAMANHO DO PRODUTO[/]\n");
        tamanho = Console.ReadLine();
        if (produtos.Any(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho))
        {
            var produto = produtos.FirstOrDefault(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho);

            var table = new Table().Expand();
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Preço[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Quantidade[/]").Centered());

            table.AddRow($"[bold white]{produto.Nome}[/]", $"[bold white]{produto.Cor}[/]", $"[bold white]{produto.Tamanho}[/]", $"[bold white]{produto.Preco}[/]", $"[bold white]{produto.Quantidade}[/]");

            table.AddEmptyRow();

            AnsiConsole.Write(table);
            AnsiConsole.Markup("[bold white]O PRODUTO ESTÁ CORRETO ( s / n ): [/]\n");

            if (Console.ReadLine() == "s")
            {
                produtos.Remove(produto);
                SalvarProdutos();
                AnsiConsole.Markup("[bold green]PRODUTO REMOVIDO COM SUCESSO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                AnsiConsole.Markup("[bold red]PRODUTO NÃO REMOVIDO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
        }
        else
        {
            throw new Exception("O PRODUTO NÃO FOI ENCONTRADO, TENTE NOVAMENTE\n");
        }

    }
    public static void FuncaoMudarNome()
    {
        VerificarProduto();
        LerProdutos();

        AnsiConsole.Markup("[bold white]DIGITE O NOME DO PRODUTO[/]\n");
        nome = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE A COR DO PRODUTO[/]\n");
        cor = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE O TAMANHO DO PRODUTO[/]\n");
        tamanho = Console.ReadLine();
        if (produtos.Any(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho))
        {
            var produto = produtos.FirstOrDefault(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho);

            var table = new Table().Expand();
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Preço[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Quantidade[/]").Centered());

            table.AddRow($"[bold white]{produto.Nome}[/]", $"[bold white]{produto.Cor}[/]", $"[bold white]{produto.Tamanho}[/]", $"[bold white]{produto.Preco}[/]", $"[bold white]{produto.Quantidade}[/]");

            table.AddEmptyRow();

            AnsiConsole.Write(table);
            AnsiConsole.Markup("[bold white]O PRODUTO ESTÁ CORRETO ( s / n ): [/]\n");

            if (Console.ReadLine() == "s")
            {
                AnsiConsole.Markup("[bold white]DIGITE O NOVO NOME DO PRODUTO[/]\n");
                nome = Console.ReadLine();
                produto.Nome = nome;
                SalvarProdutos();
                AnsiConsole.Markup("[bold green]NOME DO PRODUTO ALTERADO COM SUCESSO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                AnsiConsole.Markup("[bold red]NOME DO PRODUTO NÃO ALTERADO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
        }
        else
        {
            throw new Exception("O PRODUTO NÃO FOI ENCONTRADO, TENTE NOVAMENTE\n");
        }

    }
    public static void FuncaoMudarCor()
    {
        VerificarProduto();
        LerProdutos();

        AnsiConsole.Markup("[bold white]DIGITE O NOME DO PRODUTO[/]\n");
        nome = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE A COR DO PRODUTO[/]\n");
        cor = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE O TAMANHO DO PRODUTO[/]\n");
        tamanho = Console.ReadLine();
        if (produtos.Any(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho))
        {
            var produto = produtos.FirstOrDefault(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho);

            var table = new Table().Expand();
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Preço[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Quantidade[/]").Centered());

            table.AddRow($"[bold white]{produto.Nome}[/]", $"[bold white]{produto.Cor}[/]", $"[bold white]{produto.Tamanho}[/]", $"[bold white]{produto.Preco}[/]", $"[bold white]{produto.Quantidade}[/]");

            table.AddEmptyRow();

            AnsiConsole.Write(table);
            AnsiConsole.Markup("[bold white]O PRODUTO ESTÁ CORRETO ( s / n ): [/]\n");

            if (Console.ReadLine() == "s")
            {
                AnsiConsole.Markup("[bold white]DIGITE A NOVA COR DO PRODUTO[/]\n");
                cor = Console.ReadLine();
                produto.Cor = cor;
                SalvarProdutos();
                AnsiConsole.Markup("[bold green]COR DO PRODUTO ALTERADO COM SUCESSO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                AnsiConsole.Markup("[bold red]COR DO PRODUTO NÃO ALTERADO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
        }
        else
        {
            throw new Exception("O PRODUTO NÃO FOI ENCONTRADO, TENTE NOVAMENTE\n");
        }

    }
    public static void FuncaoMudarTamanho()
    {
        VerificarProduto();
        LerProdutos();

        AnsiConsole.Markup("[bold white]DIGITE O NOME DO PRODUTO[/]\n");
        nome = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE A COR DO PRODUTO[/]\n");
        cor = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE O TAMANHO DO PRODUTO[/]\n");
        tamanho = Console.ReadLine();
        if (produtos.Any(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho))
        {
            var produto = produtos.FirstOrDefault(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho);

            var table = new Table().Expand();
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Preço[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Quantidade[/]").Centered());

            table.AddRow($"[bold white]{produto.Nome}[/]", $"[bold white]{produto.Cor}[/]", $"[bold white]{produto.Tamanho}[/]", $"[bold white]{produto.Preco}[/]", $"[bold white]{produto.Quantidade}[/]");

            table.AddEmptyRow();

            AnsiConsole.Write(table);
            AnsiConsole.Markup("[bold white]O PRODUTO ESTÁ CORRETO ( s / n ): [/]\n");

            if (Console.ReadLine() == "s")
            {
                AnsiConsole.Markup("[bold white]DIGITE O NOVO TAMANHO DO PRODUTO[/]\n");
                tamanho = Console.ReadLine();
                produto.Tamanho = tamanho;
                SalvarProdutos();
                AnsiConsole.Markup("[bold green]TAMANHO DO PRODUTO ALTERADO COM SUCESSO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                AnsiConsole.Markup("[bold red]TAMANHO DO PRODUTO NÃO ALTERADO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
        }
        else
        {
            throw new Exception("O PRODUTO NÃO FOI ENCONTRADO, TENTE NOVAMENTE\n");
        }

    }
    public static void FuncaoMudarPreco()
    {
        VerificarProduto();
        LerProdutos();

        AnsiConsole.Markup("[bold white]DIGITE O NOME DO PRODUTO[/]\n");
        nome = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE A COR DO PRODUTO[/]\n");
        cor = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE O TAMANHO DO PRODUTO[/]\n");
        tamanho = Console.ReadLine();
        if (produtos.Any(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho))
        {
            var produto = produtos.FirstOrDefault(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho);

            var table = new Table().Expand();
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Preço[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Quantidade[/]").Centered());

            table.AddRow($"[bold white]{produto.Nome}[/]", $"[bold white]{produto.Cor}[/]", $"[bold white]{produto.Tamanho}[/]", $"[bold white]{produto.Preco}[/]", $"[bold white]{produto.Quantidade}[/]");

            table.AddEmptyRow();

            AnsiConsole.Write(table);
            AnsiConsole.Markup("[bold white]O PRODUTO ESTÁ CORRETO ( s / n ): [/]\n");

            if (Console.ReadLine() == "s")
            {
                AnsiConsole.Markup("[bold white]DIGITE O NOVO PREÇO DO PRODUTO[/]\n");
                preco = decimal.Parse(Console.ReadLine());
                produto.Preco = preco;
                SalvarProdutos();
                AnsiConsole.Markup("[bold green]PREÇO DO PRODUTO ALTERADO COM SUCESSO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                AnsiConsole.Markup("[bold red]PREÇO DO PRODUTO NÃO ALTERADO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
        }
        else
        {
            throw new Exception("O PRODUTO NÃO FOI ENCONTRADO, TENTE NOVAMENTE\n");
        }

    }
    public static void FuncaoMudarQuantidade()
    {
        VerificarProduto();
        LerProdutos();

        AnsiConsole.Markup("[bold white]DIGITE O NOME DO PRODUTO[/]\n");
        nome = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE A COR DO PRODUTO[/]\n");
        cor = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE O TAMANHO DO PRODUTO[/]\n");
        tamanho = Console.ReadLine();
        if (produtos.Any(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho))
        {
            var produto = produtos.FirstOrDefault(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho);

            var table = new Table().Expand();
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Preço[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Quantidade[/]").Centered());

            table.AddRow($"[bold white]{produto.Nome}[/]", $"[bold white]{produto.Cor}[/]", $"[bold white]{produto.Tamanho}[/]", $"[bold white]{produto.Preco}[/]", $"[bold white]{produto.Quantidade}[/]");

            table.AddEmptyRow();

            AnsiConsole.Write(table);
            AnsiConsole.Markup("[bold white]O PRODUTO ESTÁ CORRETO ( s / n ): [/]\n");

            if (Console.ReadLine() == "s")
            {
                AnsiConsole.Markup("[bold white]DIGITE A NOVA QUANTIDADE DO PRODUTO[/]\n");
                quantidade = int.Parse(Console.ReadLine());
                produto.Quantidade = quantidade;
                SalvarProdutos();
                AnsiConsole.Markup("[bold green]QUANTIDADE DO PRODUTO ALTERADO COM SUCESSO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                AnsiConsole.Markup("[bold red]QUANTIDADE DO PRODUTO NÃO ALTERADO[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
        }
        else
        {
            throw new Exception("O PRODUTO NÃO FOI ENCONTRADO, TENTE NOVAMENTE\n");
        }

    }
    public static void FuncaoSairMenuModificacao()
    {
        opMModificacao = menuModificacao.Voltar;
    }

    #endregion==============================================================


    #region Funções do Menu Relatorio
    public static void MenuRelatorio()
    {
        var table = new Table()
        .Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold white]MENU RELATORIO[/]").Centered());
        table.AddRow("[bold white]1 - RELATORIO DE PRODUTOS NO ESTOQUE[/]                                     [bold white]2 - RELATORIO GERAL DE VENDAS[/]\n").Centered();
        table.AddRow("[bold white]3 - RELATORIO ESPECIFICO DE VENDAS[/]                                 [bold white]4 - RELATORIO QUANTIDADE DE PRODUTO[/]\n").Centered();
        table.AddRow("[bold white]0 - VOLTAR[/]\n").Centered();
        AnsiConsole.Write(table);
        AnsiConsole.Markup("[bold white]DIGITE SUA OPÇÃO --> [/]");

    }
    public static void ProgramaRelatorio()
    {
        try
        {
            do
            {
                MenuRelatorio();
                opMRelatorio = Enum.Parse<menuRelatorio>(Console.ReadLine());
                Console.Clear();

                switch (opMRelatorio)
                {
                    case menuRelatorio.RelatorioProdutos:
                        FuncaoRelatorioProdutos();
                        break;
                    case menuRelatorio.RelatorioVendasGeral:
                        FuncaoRelatorioVendasGeral();
                        break;
                    case menuRelatorio.RelatorioVendasEspecifico:
                        FuncaoRelatorioVendasEspecificas();
                        break;
                    case menuRelatorio.RelatorioQuantidadeProduto:
                        FuncaoRelatorioQuantidadeProduto();
                        break;
                    case menuRelatorio.Voltar:
                        FuncaoSairMenuRelatorio();
                        break;
                    default:
                        throw new Exception("Opção do menu cadastro invalida, tente novamente.");
                }
            } while (opMRelatorio != menuRelatorio.Voltar);
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup($"[bold red]OCORREU UM ERRO INESPERADO: {ex.Message}[/]\n");
            Console.ReadKey();
            Console.Clear();
            ProgramaRelatorio();
        }

    }
    public static void FuncaoRelatorioProdutos()
    {
        LerProdutos();
        if (produtos.Count == 0)
        {
            AnsiConsole.Markup("[bold red]NENHUM PRODUTO CADASTRADO[/]\n");
            return;
        }
        var table = new Table().Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Preço[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Quantidade[/]").Centered());

        foreach (var produto in produtos)
        {
            table.AddRow($"[bold white]{produto.Nome}[/]", $"[bold white]{produto.Cor}[/]", $"[bold white]{produto.Tamanho}[/]", $"[bold white]{produto.Preco}[/]", $"[bold white]{produto.Quantidade}[/]");
            table.AddEmptyRow();
        }
        AnsiConsole.Write(table);
        Console.ReadKey();
        Console.Clear();
    }
    public static void FuncaoRelatorioVendasGeral()
    {
        LerListaDeVendas();
        if (listaDeVendas.Count == 0)
        {
            AnsiConsole.Markup("[bold red]NENHUMA VENDA REGISTRADA[/]\n");
            return;
        }
        var table = new Table().Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold yellow]Data[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Preço Unitario[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Quantidade Vendida[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Preço Total[/]").Centered());

        foreach (var item in listaDeVendas)
        {
            table.AddRow($"[bold white]{item.DataHora}[/]", $"[bold white]{item.ProdutoVendido.Nome}[/]", $"[bold white]{item.ProdutoVendido.Cor}[/]", $"[bold white]{item.ProdutoVendido.Tamanho}[/]", $"[bold white]{item.ProdutoVendido.Preco}[/]", $"[bold white]{item.QuantidadeVendida}[/]", $"[bold white]{item.ProdutoVendido.Preco * item.QuantidadeVendida}[/]");

            table.AddEmptyRow();
        }
        AnsiConsole.Write(table);
        Console.ReadKey();
        Console.Clear();
    }
    public static void FuncaoRelatorioVendasEspecificas()
    {
        LerListaDeVendas();
        var table = new Table().Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold white]DIGITE AS DATAS MANTENDO ESSE FORMATO ->[/] [bold yellow] 00/00/0000 [/] ").Centered());
        AnsiConsole.Write(table);

        AnsiConsole.Markup("[bold white]DIGITE A DATA DE INICIO[/]\n");
        string dataInicioStr = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE A DATA DE FIM[/]\n");
        string dataFimStr = Console.ReadLine();


        DateTime dataInicio = DateTime.ParseExact(dataInicioStr, "dd/MM/yyyy", null);
        DateTime dataFim = DateTime.ParseExact(dataFimStr, "dd/MM/yyyy", null);

        var produtosFiltrados = listaDeVendas.Where(p => p.DataHora >= dataInicio && p.DataHora <= dataFim).ToList();
        if (produtosFiltrados.Count == 0)
        {
            AnsiConsole.Markup("[bold red]NENHUMA VENDA REGISTRADA ENTRE AS DATAS INFORMADAS[/]\n");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        Console.Clear();

        table = new Table().Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn($"[bold white]REGISTRO DE VENDAS ENTRE ->[/] [bold yellow]{dataInicio.ToString("dd/MM/yyyy")}[/] [bold white]e[/] [bold yellow]{dataFim.ToString("dd/MM/yyyy")}[/]").Centered());
        AnsiConsole.Write(table);


        table = new Table().Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold yellow]Data[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Preço Unitario[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Quantidade Vendida[/]").Centered());
        table.AddColumn(new TableColumn("[bold yellow]Preço Total[/]").Centered());

        foreach (var item in produtosFiltrados)
        {
            table.AddRow($"[bold white]{item.DataHora}[/]", $"[bold white]{item.ProdutoVendido.Nome}[/]", $"[bold white]{item.ProdutoVendido.Cor}[/]", $"[bold white]{item.ProdutoVendido.Tamanho}[/]", $"[bold white]{item.ProdutoVendido.Preco}[/]", $"[bold white]{item.QuantidadeVendida}[/]", $"[bold white]{item.ProdutoVendido.Preco * item.QuantidadeVendida}[/]");

            table.AddEmptyRow();
        }
        AnsiConsole.Write(table);
        Console.ReadKey();
        Console.Clear();

    }
    public static void FuncaoRelatorioQuantidadeProduto()
    {
        LerProdutos();
        var quantidade = produtos.Count();
        var table = new Table().Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold yellow]Quantidade De Produtos Cadastrados[/]").Centered());
        table.AddRow($"[bold white]{quantidade} Produtos[/]");
        AnsiConsole.Write(table);
        Console.ReadKey();
        Console.Clear();
    }
    public static void FuncaoSairMenuRelatorio()
    {
        opMRelatorio = menuRelatorio.Voltar;
    }
    #endregion==============================================================


    #region Funções do Menu Caixa
    public static void MenuCaixa()
    {
        var table = new Table().Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold white]MENU CAIXA[/]").Centered());
        table.AddRow("[bold white]1 - REGISTRAR VENDA[/]                                         [bold white]2 - DEVOLUÇÃO[/]\n").Centered();
        table.AddRow("[bold white]3 - VALOR DO CAIXA[/]                                             [bold white]0 - VOLTAR[/]\n").Centered();
        AnsiConsole.Write(table);
        AnsiConsole.Markup("[bold white]DIGITE SUA OPÇÃO --> [/]");
    }
    public static void ProgramaCaixa()
    {
        try
        {
            do
            {

                MenuCaixa();
                opMCaixa = Enum.Parse<menuCaixa>(Console.ReadLine());
                Console.Clear();

                switch (opMCaixa)
                {
                    case menuCaixa.RegistrarVenda:
                        FuncaoRegistrarVenda();
                        break;
                    case menuCaixa.Devolucao:
                        FuncaoDevolverVenda();
                        break;
                    case menuCaixa.ValorCaixa:
                        FuncaoValorCaixa();
                        break;
                    case menuCaixa.Voltar:
                        FuncaoSairMenuCaixa();
                        break;

                    default:
                        throw new Exception("OPÇÃO DO MENU RELATORIO INVALIDA, TENTE NOVAMENTE");
                }
            } while (opMCaixa != menuCaixa.Voltar);
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup($"[bold red]OCORREU UM ERRO INESPERADO: {ex.Message}[/]\n");
            Console.ReadKey();
            Console.Clear();
            ProgramaCaixa();
        }

    }
    public static void FuncaoRegistrarVenda()
    {
        LerProdutos();
        LerListaDeVendas();
        LerCaixa();
        AnsiConsole.Markup("[bold white]DIGITE O NOME DO PRODUTO[/]\n");
        nome = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE A COR DO PRODUTO[/]\n");
        cor = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE O TAMANHO DO PRODUTO[/]\n");
        tamanho = Console.ReadLine();
        if (produtos.Any(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho))
        {
            var produto = produtos.FirstOrDefault(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho);

            var table = new Table().Expand();
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Preço[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Quantidade[/]").Centered());

            table.AddRow($"[bold white]{produto.Nome}[/]", $"[bold white]{produto.Cor}[/]", $"[bold white]{produto.Tamanho}[/]", $"[bold white]{produto.Preco}[/]", $"[bold white]{produto.Quantidade}[/]");

            table.AddEmptyRow();

            AnsiConsole.Write(table);
            AnsiConsole.Markup("[bold white]O PRODUTO ESTÁ CORRETO ( s / n ): [/]\n");

            if (Console.ReadLine() == "s")
            {
                AnsiConsole.Markup("[bold white]DIGITE A QUANTIDADE DE PRODUTO VENDIDO[/]\n");
                quantidade = int.Parse(Console.ReadLine());
                if (quantidade <= 0)
                {
                    AnsiConsole.Markup("[bold red]VENDA DO PRODUTO NÃO REALIZADA, QUANTIDADE DE PRODUTO INVALIDA[/]\n");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (quantidade <= produto.Quantidade)
                {

                    ListaDeVendas produtoVendido = new ListaDeVendas(produto, quantidade);
                    listaDeVendas.Add(produtoVendido);

                    produto.Quantidade -= quantidade;

                    caixa.AdicionarDinheiro(produto, quantidade);

                    SalvarListaDeVendas();
                    SalvarProdutos();
                    SalvarCaixa();

                    AnsiConsole.Markup("[bold green]VENDA DO PRODUTO REALIZADA COM SUCESSO[/]\n");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    AnsiConsole.Markup("[bold red]VENDA DO PRODUTO NÃO REALIZADA, QUANTIDADE DE PRODUTO INSUFICIENTE[/]\n");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                AnsiConsole.Markup("[bold red]VENDA DO PRODUTO NÃO REALIZADA[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
        }
        else
        {
            throw new Exception("O PRODUTO NÃO FOI ENCONTRADO, TENTE NOVAMENTE\n");
        }
    }
    public static void FuncaoDevolverVenda()
    {
        LerProdutos();
        LerListaDeVendas();
        LerCaixa();

        AnsiConsole.Markup("[bold white]DIGITE O NOME DO PRODUTO[/]\n");
        nome = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE A COR DO PRODUTO[/]\n");
        cor = Console.ReadLine();
        AnsiConsole.Markup("[bold white]DIGITE O TAMANHO DO PRODUTO[/]\n");
        tamanho = Console.ReadLine();
        if (listaDeVendas.Any(p => p.ProdutoVendido.Nome == nome && p.ProdutoVendido.Cor == cor && p.ProdutoVendido.Tamanho == tamanho))
        {
            var produtoVendido = listaDeVendas.FirstOrDefault(p => p.ProdutoVendido.Nome == nome && p.ProdutoVendido.Cor == cor && p.ProdutoVendido.Tamanho == tamanho);
            var produto = produtos.FirstOrDefault(p => p.Nome == nome && p.Cor == cor && p.Tamanho == tamanho);

            var table = new Table().Expand();
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[bold yellow]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Cor[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Tamanho[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Preço[/]").Centered());

            table.AddRow($"[bold white]{produtoVendido.ProdutoVendido.Nome}[/]", $"[bold white]{produtoVendido.ProdutoVendido.Cor}[/]", $"[bold white]{produtoVendido.ProdutoVendido.Tamanho}[/]", $"[bold white]{produtoVendido.ProdutoVendido.Preco}[/]");

            table.AddEmptyRow();

            AnsiConsole.Write(table);
            AnsiConsole.Markup("[bold white]O PRODUTO ESTÁ CORRETO ( s / n ): [/]\n");

            if (Console.ReadLine() == "s")
            {
                AnsiConsole.Markup("[bold white]DIGITE A QUANTIDADE DE PRODUTOS DA DEVOLUÇÃO[/]\n");
                quantidade = int.Parse(Console.ReadLine());
                if (quantidade <= 0)
                {
                    AnsiConsole.Markup("[bold red]DEVOLUÇÃO DO PRODUTO NÃO REALIZADA, QUANTIDADE DE PRODUTO INVALIDA[/]\n");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (produtoVendido.QuantidadeVendida == quantidade)
                {
                    listaDeVendas.Remove(produtoVendido);
                    AnsiConsole.Markup("[bold green]DEVOLUÇÃO REALIZADA COM SUCESSO[/]\n");
                    produto.Quantidade += quantidade;
                    caixa.RemoverDinheiro(produto, quantidade);

                    Console.ReadKey();
                    Console.Clear();

                }
                else if (produtoVendido.QuantidadeVendida > quantidade)
                {
                    produtoVendido.QuantidadeVendida -= quantidade;
                    AnsiConsole.Markup("[bold green]DEVOLUÇÃO REALIZADA COM SUCESSO[/]\n");
                    produto.Quantidade += quantidade;
                    caixa.RemoverDinheiro(produto, quantidade);
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    AnsiConsole.Markup("[bold red]A QUANTIDADE DE PRODUTOS DA DEVOLUÇÃO É MAIOR QUE A QUANTIDADE DE PRODUTOS VENDIDOS[/]\n");
                    Console.ReadKey();
                    Console.Clear();
                }
                SalvarListaDeVendas();
                SalvarProdutos();
                SalvarCaixa();
            }
            else
            {
                AnsiConsole.Markup("[bold red]DEVOLUÇÃO DO PRODUTO NÃO REALIZADA[/]\n");
                Console.ReadKey();
                Console.Clear();
            }
        }
        else
        {
            throw new Exception("O PRODUTO NÃO FOI ENCONTRADO, TENTE NOVAMENTE\n");
        }

    }
    public static void FuncaoValorCaixa()
    {
        LerCaixa();
        var table = new Table().Expand();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold yellow]Valor Do Caixa[/]").Centered());
        table.AddRow($"[bold white]R${caixa._valorCaixa}[/]");
        AnsiConsole.Write(table);
        Console.ReadKey();
        Console.Clear();
    }
    public static void FuncaoSairMenuCaixa()
    {
        opMCaixa = menuCaixa.Voltar;
    }

    #endregion==============================================================


    #region Funções Gerais
    public static void VerificarProduto()
    {
        if (File.Exists("produtos.json"))
        {
            string json = File.ReadAllText("produtos.json");
            produtos = JsonConvert.DeserializeObject<List<Produto>>(json) ?? new List<Produto>();
        }
        else
        {
            AnsiConsole.Markup($"[bold red]Erro no banco de Dados(PRODUTOS)[/]\n");
            Console.WriteLine("");
        }
    }
    public static void VerificarListaDeVendas()
    {
        if (File.Exists("listaDeVendas.json"))
        {
            string json = File.ReadAllText("listaDeVendas.json");
            listaDeVendas = JsonConvert.DeserializeObject<List<ListaDeVendas>>(json) ?? new List<ListaDeVendas>();
        }
        else
        {
            AnsiConsole.Markup($"[bold red]Erro no banco de Dados(LISTA DE VENDAS)[/]\n");
            Console.WriteLine("");
        }
    }
    public static void VerificarCaixa()
    {
        if (File.Exists("caixa.json"))
        {
            string json = File.ReadAllText("caixa.json");
            caixa = JsonConvert.DeserializeObject<Caixa>(json) ?? new Caixa();
        }
        else
        {
            AnsiConsole.Markup($"[bold red]Erro no banco de Dados(CAIXA)[/]\n");
            Console.WriteLine("");
        }
    }
    public static void SalvarProdutos()
    {
        novoJson = JsonConvert.SerializeObject(produtos, Formatting.Indented);
        File.WriteAllText("produtos.json", novoJson);
    }
    public static void LerProdutos()
    {
        json = File.ReadAllText("produtos.json");
        List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(json);
    }
    public static void SalvarListaDeVendas()
    {
        novoJson = JsonConvert.SerializeObject(listaDeVendas, Formatting.Indented);
        File.WriteAllText("listaDeVendas.json", novoJson);
    }
    public static void LerListaDeVendas()
    {
        json = File.ReadAllText("listaDeVendas.json");
        List<ListaDeVendas> listaDeVendas = JsonConvert.DeserializeObject<List<ListaDeVendas>>(json);
    }
    public static void SalvarCaixa()
    {
        novoJson = JsonConvert.SerializeObject(caixa, Formatting.Indented);
        File.WriteAllText("caixa.json", novoJson);
    }
    public static void LerCaixa()
    {
        json = File.ReadAllText("caixa.json");
        caixa = JsonConvert.DeserializeObject<Caixa>(json);
    }

    #endregion==============================================================

}