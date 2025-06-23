﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AppRpgEtec.Models;
using AppRpgEtec.Models.Enums;
using AppRpgEtec.Services.Personagens;

namespace AppRpgEtec.ViewModels.Personagens;
   
[QueryProperty("PersonagemSelecionadoId", "pId")]
public class CadastroPersonagemViewModel : BaseViewModel
{
    private PersonagemService pService;
    private ObservableCollection<TipoClasse> listaTiposClasse;
    private TipoClasse tipoClasseSelecionado;

    public ICommand SalvarCommand { get; }
    public ICommand CancelarCommand { get; set;  }

    public ObservableCollection<TipoClasse> ListaTiposClasse
    {
        get { return listaTiposClasse; }
        set
        {
            if (value != null)
            {
                listaTiposClasse = value;
                OnPropertyChanged();
            }
        }
    }
    public TipoClasse TipoClasseSelecionado
    {
        get { return tipoClasseSelecionado; }
        set
        {
            if (value != null)
            {
                TipoClasseSelecionado = value;
                OnPropertyChanged();
            }
        }
    }

    private int id;
    private string nome;
    private int pontosVida;
    private int forca;
    private int defesa;
    private int inteligencia;
    private int disputas;
    private int vitorias;
    private int derrotas;
    private string personagemSelecionadoId;

    public string PersonagemSelecionadoId
    {
        set
        {
            if (value != null)
            {
                personagemSelecionadoId = Uri.UnescapeDataString(value);
                //CarregarPersonagem();
            }
        }
    }

    public int Id { get => id; set { id = value; OnPropertyChanged(); } }
    public string Nome { get => nome; set { nome = value; OnPropertyChanged(); } }
    public int PontosVida { get => pontosVida; set { pontosVida = value; OnPropertyChanged(); } }
    public int Forca { get => forca; set { forca = value; OnPropertyChanged(); } }
    public int Defesa { get => defesa; set { defesa = value; OnPropertyChanged(); } }
    public int Inteligencia { get => inteligencia; set { inteligencia = value; OnPropertyChanged(); } }
    public int Disputas { get => disputas; set { disputas = value; OnPropertyChanged(); } }
    public int Vitorias { get => vitorias; set { vitorias = value; OnPropertyChanged(); } }
    public int Derrotas { get => derrotas; set { derrotas = value; OnPropertyChanged(); } }

    public CadastroPersonagemViewModel()
    {
        string token = Preferences.Get("UsuarioToken", string.Empty);
        pService = new PersonagemService(token);
        _ = ObterClasses();

        SalvarCommand = new Command(async () => { await SalvarPersonagem(); });
        CancelarCommand = new Command(async => CancelarCadastro());
    }

    public async Task ObterClasses()
    {
        try
        {
            ListaTiposClasse = new ObservableCollection<TipoClasse>();
            ListaTiposClasse.Add(new TipoClasse() { Id = 1, Descricao = "Cavaleiro" });
            ListaTiposClasse.Add(new TipoClasse { Id = 2, Descricao = "Mago" });
            ListaTiposClasse.Add(new TipoClasse() { Id = 3, Descricao = "Clerigo" });
            OnPropertyChanged(nameof(ListaTiposClasse));
        }
        catch (Exception ex) {
            await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
        }
    }

    public async Task SalvarPersonagem()
    {
        try
        {
            Personagem model = new Personagem()
            {
                Nome = this.nome,
                PontosVida = this.pontosVida,
                Defesa = this.defesa,
                Derrotas = this.derrotas,
                Disputas = this.disputas,
                Forca = this.forca,
                Inteligencia = this.inteligencia,
                Vitorias = this.vitorias,
                Id = this.id,
                Classe = (ClasseEnum)tipoClasseSelecionado.Id
            };

            if (model.Id == 0)
                await pService.PostPersonagemAsync(model);
            else
                await pService.PutPersonagemAsync(model);

            await Application.Current.MainPage
                .DisplayAlert("Mensagem", "Dados Salvos com sucesso", "Ok");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
        }
    }

    private async void CancelarCadastro()
    {
        await Shell.Current.GoToAsync("..");
    }

    public async void CarregarPersonagem()
    {
        try
        {
            Personagem p = await pService.GetPersonagemAsync(int.Parse(personagemSelecionadoId));

            this.Nome = p.Nome;
            this.PontosVida = p.PontosVida;
            this.Defesa = p.Defesa;
            this.Derrotas = p.Derrotas;
            this.Disputas = p.Disputas;
            this.Forca = p.Forca;
            this.Inteligencia = p.Inteligencia;
            this.Vitorias = p.Vitorias;
            this.Id = p.Id;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
        }
    }
}
