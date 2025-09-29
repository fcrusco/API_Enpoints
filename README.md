# 📌 API de Produtos – CRUD em Memória

<img width="941" height="807" alt="image" src="https://github.com/user-attachments/assets/a8640215-adb7-445b-9c50-9807d68558f5" />


Este é um projeto simples em **ASP.NET Core 8 Web API** criado para fins didáticos.  
Ele demonstra os fundamentos de uma API com **endpoints CRUD (Create, Read, Update, Delete)** utilizando dados armazenados **em memória** (sem banco de dados).

---

## 🚀 Tecnologias utilizadas
- [.NET 8](https://dotnet.microsoft.com/)  
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api)  
- [Swagger / Swashbuckle](https://learn.microsoft.com/aspnet/core/tutorials/getting-started-with-swashbuckle) (documentação automática da API)

---

## 📂 Estrutura do Projeto

```
MinhaAPI/
│── Controllers/
│   └── ProdutosController.cs   # Endpoints da API
│── Model/
│   └── Produto.cs              # Modelo de dados
│── Program.cs                  # Configuração inicial da aplicação
│── appsettings.json            # Configurações da aplicação
```

---

## 📖 Endpoints disponíveis

### 🔹 Listar todos os produtos
```http
GET /api/produtos
```
✔ Retorna a lista de produtos em memória.  

---

### 🔹 Buscar produto por ID
```http
GET /api/produtos/{id}
```
✔ Retorna um produto específico pelo seu `id`.  
❌ Se não encontrar, retorna **404 Not Found**.  

---

### 🔹 Criar um novo produto
```http
POST /api/produtos
```
📥 Exemplo de body (JSON):
```json
{
  "nome": "Camiseta",
  "preco": 50
}
```
✔ Retorna **201 Created** com o produto criado e a URL para consulta (`Location`).  
❌ Se os dados forem inválidos, retorna **400 Bad Request**.  

---

### 🔹 Atualizar um produto existente
```http
PUT /api/produtos/{id}
```
📥 Exemplo de body (JSON):
```json
{
  "nome": "Tênis Esportivo",
  "preco": 199
}
```
✔ Retorna **204 No Content** se atualizado com sucesso.  
❌ Se não encontrar, retorna **404 Not Found**.  

---

### 🔹 Deletar um produto
```http
DELETE /api/produtos/{id}
```
✔ Retorna **204 No Content** se deletado.  
❌ Se não encontrar, retorna **404 Not Found**.  

---

## 🛠 Como executar o projeto
1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/minha-api.git
   cd minha-api
   ```

2. Restaure os pacotes e rode a aplicação:
   ```bash
   dotnet restore
   dotnet run
   ```

3. Acesse no navegador ou no Postman:
   ```
   https://localhost:7261/swagger
   ```

---

## 🎯 Objetivo do Projeto
Este projeto foi desenvolvido para **ensinar os conceitos iniciais de APIs em C#**:
- Estrutura mínima de uma API.  
- Como funcionam os **verbos HTTP (GET, POST, PUT, DELETE)**.  
- Uso do **Swagger** para documentação.  
- Manipulação de dados em memória (sem persistência).  

---

📚 Documentação recomendada:
- [Documentando APIs com Swagger](https://learn.microsoft.com/aspnet/core/tutorials/getting-started-with-swashbuckle)  
- [Criando sua primeira Web API em .NET](https://learn.microsoft.com/aspnet/core/web-api/first-web-api)  
