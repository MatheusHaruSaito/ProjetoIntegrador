# ProjetoIntegradorAngular

Unolink é um projeto desenvolvido 

EXECUTAR O PROJETO NA API:
1 - Configurar o projeto de inicialização único para a "unolink.api", aplicar e confirmar;
2 - Ir até para a "Program.cs" e colocar a ConnectionString desejada na 24º linha. Se quiser usar uma conexão própria, basta adicioná-la no "appsettings.json" - Utilizar uma conexão do SQL Server;
3 - Após concluir o passo 2, abrir o Console do Gerenciador de Pacotes (Nuget) e trocar o Projeto Patrão para "unolink.infrastructure";
4 - Execute o comando "Update-Database" ainda no mesmo Console.

Se todos os passos forem seguidos corretamente, a API poderá ser executada sem problemas.

EXECUTAR O PROJETO NA UI:
1 - Abra o Terminal e utilize o da sua própria escolha: Git Bash ou Command Prompt;
2 - Execute "npm install" para que os pacotes angular possam ser instalados;
3 - Execute "ng s" para iniciar o projeto, e já estará pronto para uso.

Quando a API e a UI estiverem executando corretamente, é quando o site estará pronto para ser utilizado sem problemas!
