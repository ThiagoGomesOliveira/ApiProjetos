# ApiProjetos

Objetivo deste projeto foi a criação de uma API para criação de projetos e atividades. Este projeto foi divido em três camadas, 
Model, Business e Api.
Camada de model é aonde fica as entidades relacionadas com o banco  de dados, neste projeto foi utilizado uma dll chamada ORM, aonde é realizado todo o CRUD com o banco de dados.
As entidades da camada da Model precisam herdar a classe Service da dll ORM, para assim herdarem os métodos de CRUD.
Em cada entidade é necessário informar Atribute em suas propriedades, conforme imagem abaixo:

![image](https://user-images.githubusercontent.com/33624004/136717286-9322d819-f0fb-4bc1-a882-01698e81b4b1.png)

Para poder conectar o banco de dados ao projeto é necessário informar a connection string no arquivo appsettings.json:


![image](https://user-images.githubusercontent.com/33624004/136717200-7f4dda7e-9c28-421c-aeaf-9310d29f08a8.png)

Para utilizar o swagger é necessário marcar o checkbox arquivo de documentação XML, conforme imagem:

![image](https://user-images.githubusercontent.com/33624004/136717461-80c671b8-4e88-45bf-a700-4afdd83e0fd0.png)



Neste  projeto foi utilizado as  seguintes tecnologias: 
 .Net core
 .Sql Server
 .Swagger 
