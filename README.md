# SOSYS

Iniciei a criação desse projeto já faz algum tempo com o objetivo de ter uma base legal já desenvolvida, para quando pegar projetos
que necessitem dos conceitos e arquitetura utilizados aqui, já ter um ponto de partida. Na época devido ao trabalho acabei
deixando esse projeto de lado, porém agora estou retomando com o objetivo de ter uma API para utilizar em meus projetos de estudo React, 
Angular, entre outros, ter um projeto legal para apresentar quando estiver em busca de novas oportunidades e ter um projeto onde
aplico diversos conceitos.

Dessa forma acredito que posso ajudar amigos e colegas de trabalho que são que desenvolvedores junior, pleno ou 
seniors, que gostariam de se atualizar e aprender alguns dos conceitos aplicados nesse projeto. Tendo uma aplicação rodando acredito que seja o melhor caminho para explicar e realizar demonstrações, e ter um material de apoio.

Nesse projeto foram aplicados diversos conceitos e ferramentas como: MVC, DDD, TDD, Web API, JWT, Identity (isolado), Logs, IoC, 
swagger, entre outros.

#### Como Executar
1 - Instale o Docker

2 - Inicie o mongo (docker run -d p- 27017:27017 mongo)

3 - Inicie o rabbit (docker run -d --hostname my-rabbit --name rabbit13 -p 8080:15672 -p 5672:5672 -p 25676:25676 rabbitmq:3-management)

4 - Inicie a API: LT.SO.Services.Api

5 - Inicie o service: LT.SO.Services.Gerencial.Usuarios

Obs: Swagger instalado na API, features de cadastro de usuário e login concluídas.

### Roadmap 1
Ok - Alterar o Banco de LOGS para MongoDB

Ok - Aplicar CQRS no projeto a partir do Domain "Gerencial", nas funcionalidades básicas do usuário.
  
  Ok -- Salvar os eventos lançados no MongoDb
  
Ok - Implementar RabbitMQ - Existe apenas um ServiceBus in memory, utilizar o client do RabbitMQ (RawRabbit) para criar um novo.

Ok - Criar o Service para fazer subscribe nos tópicos do Rabbit, relacionados ao módulo Gerencial.

Ok - Criar o handler e persistir as informações 

- Dockerizing

### Roadmap 2
- Criar uma nova unidade de negócio na aplicação para criar contas bancárias e realizar transferências.

  -- Conta Corrente (Numero, Usuario e Saldo)
  
    --- Funcionalidades: Cadastrar / Editar / Desativar / Consultar contas por usuário
    
  -- Transferencia (Id, ContaOrigem, ContaDestino, Valor)
  
    --- Consultar transferencias por ContaOrigem e intervalo de data
    
    --- Criar transferencia entre contas
    
      ---- Solicitar conta origem, conta destino e valor
      
      ---- Checar se as contas existem
      
      ---- Checar se existe saldo para a transferencia
      
- Criar um front em ReactJS para consumir essa nova unidade de negócio.

  -- Realizar autenticação
  
  -- Apresentar ao usuários as contas do mesmo
  
    --- Histórico de transações da conta (filtro por data)
    
    --- Fazer Transferência
    
### Roadmap 3
 - Criar mecanismo para acompanhar em tempo real o status da Transferência
 - Notificar o usuário quando o status da transação mudar

Obs: No domain gerencial eu estava implementando uma forma 'genérica' de cadastrar permissões, grupos de acesso, etc. Ainda não 
conclui essa parte e não vou finalizar por hora, vou focar no roadmap acima e depois de acordo com a necessidade do negócio que eu for
implementar, eu continuarei essas funcionalidades.
