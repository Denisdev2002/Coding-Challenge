# Ferramenta de Contagem de Palavras em C#

📑Este projeto é uma implementação personalizada da ferramenta de linha de comando Unix `wc` (word count) em C#. Ele permite contar o número de linhas, palavras, bytes e caracteres em arquivos de texto ou a partir da entrada padrão (stdin), com a flexibilidade de exibir um "nome de arquivo" especificado pelo usuário na saída, mesmo quando a entrada é via stdin.

## Funcionalidades Implementadas

***Contagem de Linhas (`-l`):** Exibe o número de linhas (quebras de linha) na entrada.
***Contagem de Palavras (`-w`):** Exibe o número de palavras na entrada, delimitadas por espaços, tabulações e quebras de linha.
***Contagem de Bytes (`-c`):** Exibe o número de bytes na entrada, utilizando a codificação UTF-8.
***Contagem de Caracteres (`-m`):** Exibe o número de caracteres na entrada, contando corretamente caracteres multibyte.
***Opções Combinadas:** Suporta a combinação de múltiplas opções em um único argumento (ex: `-lw`).
***Comportamento Padrão:** Se nenhuma opção for especificada e um arquivo for fornecido, exibe a contagem de linhas, palavras e bytes.
***Entrada Padrão (stdin):** Lê a entrada de texto através do stdin quando nenhum arquivo é especificado ou quando um "nome de arquivo" é fornecido após a opção.
***"Nome de Arquivo" Personalizado na Saída:** Permite especificar um texto após a opção que será exibido como o nome do arquivo na saída, mesmo que a entrada seja do stdin.

## Como Usar

Para executar o `ccwc`, você precisará ter o SDK do .NET instalado. Navegue até o diretório do projeto no seu terminal e use o comando `dotnet run` seguido das opções desejadas e, opcionalmente, um nome para exibir como o arquivo.

    dotnet run <opção(ões)> [nome_para_exibir]
Observações Importantes:

Se você fornecer um caminho para um arquivo existente como o último argumento, o programa tentará ler o conteúdo desse arquivo.
Se você fornecer um texto após a opção que não seja um caminho de arquivo existente, esse texto será usado como o "nome do arquivo" na saída, e o programa esperará a entrada via stdin (você pode usar pipes (|) ou redirecionamento (<)).

📌Opções
```-l: Contar linhas.
-w: Contar palavras.
-c: Contar bytes.
-m: Contar caracteres.
```
Exemplos de Uso
Contar linhas de um arquivo chamado documento.txt:

    dotnet run -l documento.txt

Saída esperada:

       123 documento.txt

Contar palavras e bytes da entrada padrão, exibindo "meu_texto" como nome do arquivo:

echo "isto é um teste" | dotnet run -wc meu_texto
Saída esperada:

     4          16 meu_texto
Contar caracteres de um arquivo chamado artigo.md:

    dotnet run -m artigo.md

Saída esperada:

      1500 artigo.md

Contar linhas, palavras e bytes da entrada padrão, exibindo "entrada_direta" como nome do arquivo:

    echo "linha 1\nlinha 2" | dotnet run entrada_direta

Saída esperada:

           2          4          16 entrada_direta

📌Estrutura do Código
O projeto consiste em duas classes principais:

OWCTools: Contém os métodos para realizar as operações de contagem (bytes, palavras, linhas, caracteres) e para exibir a mensagem de ajuda.
Program: A classe principal com o método Main, responsável por processar os argumentos da linha de comando, identificar as opções solicitadas e o "nome do arquivo" para a saída, ler a entrada (de arquivo ou stdin), chamar os métodos de contagem e exibir os resultados formatados.

***Observações sobre a Implementação***

A contagem de palavras usa espaços, tabulações e quebras de linha como delimitadores.
A contagem de caracteres para entrada padrão utiliza System.Globalization.StringInfo para lidar corretamente com caracteres Unicode.
O programa prioriza a leitura do conteúdo de um arquivo se um caminho válido for fornecido como o último argumento. Caso contrário, assume entrada padrão e usa o último argumento como o nome a ser exibido.
