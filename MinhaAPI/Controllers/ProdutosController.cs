// Importa tipos relacionados a HTTP (StatusCode, HttpContext, etc.)
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

// Importa tipos MVC (ControllerBase, atributos [ApiController], [Route], resultados de ação)
using Microsoft.AspNetCore.Mvc;
// Importa a classe Produto (o nosso modelo de dados)
using MinhaAPI.Model;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Define o namespace (organização lógica de classes no projeto)
namespace MinhaAPI.Controllers
{
    // Informa ao ASP.NET Core que esta classe é uma API Controller 
    [ApiController]
    // Define o padrão de rota: "api/{nomeDaControllerSemSufixo}" Ex.: ProdutosController -> /api/produtos
    [Route("api/[controller]")]
    // Declara a classe da controller herdando de ControllerBase (funcionalidades básicas de API)
    public class ProdutosController : ControllerBase
    {

        // Comentário: criamos uma lista em memória para simular um "banco de dados" temporário
        // Armazenamento estático: os dados duram enquanto a aplicação estiver em execução
        private static readonly List<Produto> _produtos = new();
        // Contador simples para gerar IDs incrementais
        private static int _nextId = 1;


        // Comentário: endpoint para listar todos os produtos
        // Mapeia requisições HTTP GET para /api/produtos
        [HttpGet]
        // Retorna um IActionResult (permite retornar diferentes status codes e corpos)
        //IActionResult é uma interface que representa o resultado de uma ação em uma Controller.
        //IActionResult é como dizer:"Minha resposta pode ser qualquer coisa (um número, um texto, uma caixa de presente…)."
        public IActionResult Get()
        {
            // Retorna 200 OK com a lista de produtos no corpo da resposta
            return Ok(_produtos);
        }


        // Comentário: endpoint para obter um produto específico pelo ID
        //[HttpGet(...)] Informa que este método da Controller responde a requisições HTTP GET.
        //Exemplo: quando alguém chama GET /api/produtos/5.
        //"{id:int}" Define um parâmetro de rota chamado id.
        //   id é o nome do parâmetro que será passado para o método.
        //   :int é uma restrição de tipo(Route Constraint). Só aceita números inteiros.
        [HttpGet("{id:int}")]
        //ActionResult é um tipo de retorno usado nos métodos das Controllers.
        //Ele representa qualquer resposta HTTP: 200 OK, 404 Not Found, 400 Bad Request, 201 Created, etc.
        //ActionResult<Produto> é como dizer:"Minha resposta normalmente é um tio Produto, mas pode ser outra coisa se der erro
        public ActionResult<Produto> GetById(int id)
        {
            // Procura o primeiro produto cujo Id corresponda ao informado (ou null se não achar)
            var produto = _produtos.FirstOrDefault(p => p.Id == id);
            // Se não encontrou, devolve 404 Not Found com uma mensagem
            if (produto is null) return NotFound("Produto não encontrado.");
            // Se encontrou, devolve 200 OK com o produto
            return Ok(produto);
        }


        // Comentário: endpoint para criar um novo produto
        // Mapeia POST em /api/produtos (envia os dados no corpo da requisição)
        [HttpPost]
        // Retorna o Produto criado (no corpo) e pode incluir cabeçalhos (Location)
        // com a URL do novo recurso
        public ActionResult<Produto> Post(Produto dto)
        {
            // Validação: nome obrigatório (nem vazio, nem só espaços)
            if (string.IsNullOrWhiteSpace(dto.Nome))
                // 400 Bad Request se inválido
                return BadRequest("Nome é obrigatório.");
            // Validação: preço não pode ser negativo
            if (dto.Preco < 0)
                // 400 Bad Request se inválido
                return BadRequest("Preço inválido.");

            // Gera um novo Id e atribui ao objeto recebido
            dto.Id = _nextId++;
            
            // Adiciona o produto à "base" em memória
            _produtos.Add(dto);

            //Retorna 201 Created + cabeçalho Location apontando para o GET by Id do recurso criado
            //nameof(GetById) devolve a string "GetById"
            //Refatoração segura: se você renomear o método GetById no futuro,
            //  o nameof(GetById) será atualizado pelo Visual Studio/Refactor automaticamente.
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);

            //nameof(GetById) retorna a string "GetById".
            //Isso indica qual método da controller deve ser usado para gerar a rota.
            //new { id = dto.Id } preenche os parâmetros da rota.
            //Como o GetById tem[HttpGet("{id:int}")], o ASP.NET vai substituir {id} pelo valor real(ex.: 1).
            //dto é o objeto que vai no body da resposta.
        }


        // Comentário: endpoint para atualizar um produto existente (substituição total)
        // Mapeia PUT em /api/produtos/{id}
        [HttpPut("{id:int}")]
        // Retorna apenas status (NoContent/NotFound/BadRequest)
        public ActionResult Put(int id, Produto dto)
        {
            // Tenta localizar o produto a ser atualizado
            var existe = _produtos.FirstOrDefault(p => p.Id == id);
            // Se não existir, 404 Not Found
            if (existe is null) return NotFound("Produto não encontrado.");

            // Validação: nome obrigatório
            if (string.IsNullOrWhiteSpace(dto.Nome))
                // 400 Bad Request
                return BadRequest("Nome é obrigatório.");
            // Validação: preço válido
            if (dto.Preco < 0)
                // 400 Bad Request
                return BadRequest("Preço inválido.");

            // Atualiza os campos do produto encontrado
            existe.Nome = dto.Nome;
            existe.Preco = dto.Preco;

            // retornar Ok(existente) para 200 com o objeto atualizado)
            return Ok(existe);

            //alternativa: retorna 204 No Content (atualização concluída, sem corpo na resposta)
            //return NoContent();
        }


        // Comentário: endpoint para excluir um produto pelo ID
        // Mapeia DELETE em /api/produtos/{id}
        [HttpDelete("{id:int}")]
        // Retorna apenas status (NoContent/NotFound)
        public ActionResult Delete(int id)
        {
            // Procura o produto pelo ID
            var produto = _produtos.FirstOrDefault(p => p.Id == id);

            // Se não encontrar, 404 Not Found
            if (produto is null) return NotFound("Produto não encontrado.");

            // Remove o produto da lista
            _produtos.Remove(produto);

            // Indica sucesso sem conteúdo no corpo da resposta
            return NoContent();
        }
    }
}
