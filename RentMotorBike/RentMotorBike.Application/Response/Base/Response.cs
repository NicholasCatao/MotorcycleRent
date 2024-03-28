using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Domain.Response.Base
{
     public class Response<TDados> 
    {
        private readonly IList<string> _mensagens = new List<string>();

        private readonly bool possuiErro;

        public Response()
        {
            Dados = default;
            MotivoErro = null;
        }


        public Response(MotivoErro motivoFalha, string mensagemErro)
        {
            MotivoErro = motivoFalha;

            _mensagens.Add(string.IsNullOrWhiteSpace(mensagemErro) ? nameof(motivoFalha): mensagemErro);

            DetalheErro = _mensagens.Any() ? string.Join(" | ", _mensagens.ToList()) : string.Empty;
        }

        public Response(MotivoErro motivoFalha)
        {
            Dados = default;
            MotivoErro = motivoFalha;

            possuiErro = true;
        }

        public Response(TDados dados)
        {
            Dados = dados;
            DetalheErro = string.Empty;
            MotivoErro = null;
        }

        public string DetalheErro { get; set; }

        public TDados Dados { get; set; }

        public bool PossuiErro => (!string.IsNullOrWhiteSpace(DetalheErro) || possuiErro);

        public MotivoErro? MotivoErro { get; private set; }


        public void DefinirMotivoErro(MotivoErro motivoFalha)
        {
            MotivoErro = motivoFalha;
        }
    }
}
