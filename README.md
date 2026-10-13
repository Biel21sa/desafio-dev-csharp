# Desafio de Desenvolvimento — C#

Projeto desenvolvido em C# como solução para um desafio técnico de desenvolvimento de software, utilizando .NET e arquivos JSON para entrada de dados.

## Funcionalidades

### 1. Cálculo de comissões
- Leitura de dados de vendas a partir de um arquivo JSON.
- Cálculo de comissão individual conforme o valor de cada venda.
- Agrupamento das vendas por vendedor.
- Exibição do total vendido e do total de comissão por vendedor.

**Regras de comissão:**

| Valor da venda | Comissão |
|---|---:|
| Abaixo de R$ 100,00 | 0% |
| A partir de R$ 100,00 e abaixo de R$ 500,00 | 1% |
| A partir de R$ 500,00 | 5% |

### 2. Controle de movimentação de estoque
- Carregamento dos produtos a partir de um arquivo JSON.
- Registro de entradas e saídas de produtos.
- Geração de identificador único para cada movimentação.
- Registro da descrição, tipo e quantidade movimentada.
- Consulta do estoque atualizado.
- Consulta do histórico de movimentações.
- Validação de quantidades e prevenção de saídas superiores ao estoque disponível.

### 3. Cálculo de juros por atraso
- Entrada do valor da dívida e da data de vencimento.
- Cálculo dos dias de atraso em relação à data atual.
- Aplicação de juros simples de 2,5% ao dia sobre o valor original.
- Exibição dos juros calculados e do valor total atualizado.

## Tecnologias utilizadas

- C#
- .NET 10
- Programação Orientada a Objetos (POO)
- Manipulação de arquivos JSON
- LINQ
- Git e GitHub

## Estrutura do projeto

```text
DesafioDev/
├── DesafioDev.App/
│   ├── Data/
│   │   ├── vendas.json
│   │   └── estoque.json
│   ├── Models/
│   ├── Services/
│   ├── Program.cs
│   └── DesafioDev.App.csproj
├── .gitignore
├── DesafioDev.sln
└── README.md
```

## Pré-requisitos

- .NET SDK compatível com o projeto.
- Git, caso queira clonar e versionar o repositório.

## Como executar

1. Clone o repositório:

   ```bash
   git clone URL_DO_REPOSITORIO
   ```

2. Acesse a pasta do projeto:

   ```bash
   cd DesafioDev
   ```

3. Execute a aplicação:

   ```bash
   dotnet run --project DesafioDev.App
   ```

4. Siga as instruções exibidas no terminal para interagir com as funcionalidades disponíveis.

## Observações

- Os dados iniciais de vendas e estoque são carregados de arquivos JSON.
- As movimentações de estoque são mantidas em memória durante a execução da aplicação.
- O cálculo de juros considera juros simples de 2,5% por dia de atraso, sem cobrança de juros quando a data de vencimento ainda não passou.

## Autor

Desenvolvido por **Gabriel Silva Alves** como parte de um desafio técnico de desenvolvimento de software.
