# SOSYS

Iniciei a criação desse projeto já faz algum tempo com o objetivo de ter uma base legal já desenvolvida, para quando pegar projetos
que necessitem dos conceitos e arquitetura utilizados aqui, já ter um ponto de partida. Na época devido ao trabalho acabei
deixando esse projeto de lado, porém agora estou retomando com o objetivo de ter uma API para utilizar em meus projetos de estudo React, 
Angular, entre outros, ter um projeto legal para apresentar quando estiver em busca de novas oportunidades e ter um projeto onde
aplico diversos conceitos, de forma que eu possa ajudar amigos e colegas de trabalho que são que desenvolvedores junior, pleno ou 
seniors que gostariam de se atualizar e aprender algum dos conceitos aplicados. Tendo uma aplicação rodando acredito que ajuda bastante
na hora de explicar e serve também como material de apoio.

Nesse projeto foram aplicados diversos conceitos e ferramentas como: MVC, DDD, TDD, Web API, JWT, Identity (isolado), Logs, IoC, 
swagger, entre outros.

### Roadmap 1
- Alterar o Banco de LOGS para MongoDB
- Aplicar CQRS no projeto a partir do Domain "Gerencial", nas funcionalidades básicas do usuário.
  -- Salvar os comandos em um banco MongoDb separado
- Implementar RabbitMQ - Alterar o ServiceBus atual in memory, e utilizar o client do RabbitMQ (RawRabbit).
- Criar o Service para fazer subscribe nos tópicos do módulo Gerencial, que serão criados no Rabbit e persistir as informações.
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
