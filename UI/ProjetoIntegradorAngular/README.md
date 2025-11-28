# ProjetoIntegradorAngular

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




README do próprio Angular:

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 19.2.0.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
