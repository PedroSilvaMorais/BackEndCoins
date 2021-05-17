## Projeto ApiCoins

> Nota: `O projeto desenvolvido utilizando o .NET Core 3.1. `

> Nota: `Certifique-se de verificar o localhost. `

> Nota: `Certifique-se de verificar se está executando chamadas HTTPS ou HTTP`

| Endpoint | Método |
| ------ | ------ |
| [GET]"api/moedas" | GetItemFila() |
| [POST]"api/moedas" | "AddItensFila()" |

```sh
A chamada para o método [GET] é https://localhost:44319/api/moedas.
Lembre-se de verificar o localhost no projeto
```

## Padrão do objeto Json esperado
[POST] localhost:44319/api/moedas
```sh
[
 {
 "moeda": "USD",
 "data_inicio": "2010-01-01",
 "data_fim": "2010-12-01"
 },
 {
 "moeda": "EUR",
 "data_inicio": "2020-01-01",
 "data_fim": "2010-12-01"
 },
 {
 "moeda": "JPY",
 "data_inicio": "2000-03-11",
 "data_fim": "2000-03-30"
 }
]
```

## Projeto Job

> Nota: `O projeto desenvolvido utilizando o .NET Core 3.1. `

> Nota: `Mudar o valor da variável "apiUrl" que está na classe "Program", para o localhost do projeto ApiCoins`

Sua declaração está como:
```sh
private const string apiUrl = "https://localhost:44319/api/moedas";
```
> Com o projeto ApiCoins em execução, execute o projeto Job.
Caso queira parar a execução do projeto Rotina, aperta as teclas Ctrl + C.

## Execução do Teste
> Após certificar-se o localhost, vistas acima basta executar o projeto ApiCoins
> 
> Em seguida execute o projeto Job
> 

- Nota: `Recomendo que tenha dados no banco para poder gerar o arquivo "Resultado_aaaammdd_HHmmss.csv"`

- Nota: `Os arquivos de entrada "DadosCotacao.csv" e "DadosMoeda.csv" estão no diretório de execução "Job\JobMoedas\bin\Debug\netcoreapp3.1\"` O arquivo "Resultado_aaaammdd_HHmmss.csv" gerado após a execução do projeto com um retorno da ApiCoins também ficará nesta pasta.

- Nota: `Evite manter o arquivos "DadosCotacao.csv" e "DadosMoeda.csv" aberto durante a execução da aplicação Rotina.` 