# Documentação do Projeto

## Pré-requisitos

Antes de iniciar, certifique-se de ter:

- Docker instalado em sua máquina
- As seguintes portas disponíveis em seu sistema:
  - 8080 (API)
  - 9090 (Prometheus)
  - 3000 (Grafana)
  - 3307 (MySQL)

## Iniciando o Projeto

1. No diretório raiz do projeto, execute o seguinte comando para iniciar todos os serviços:

```bash
docker compose up --build -d
```

Este comando irá construir e iniciar os seguintes containers:
- API
- MySQL
- Prometheus
- Grafana

## Configurando o Grafana

1. Acesse o Grafana através do navegador:
```
http://localhost:3000
```

2. Faça login utilizando as seguintes credenciais:
```
Usuário: admin
Senha: admin
```

3. Configure o Prometheus como fonte de dados:
   - Acesse as configurações de Data Sources
   - Clique em "Add new data source"
   - Selecione "Prometheus"
   - No campo URL, insira: `http://prometheus:9090`
   - Clique em "Save & Test"

4. Importe o dashboard pré-configurado:
   - Localize o arquivo `grafana-dashboard-config.json` na pasta raiz do projeto
   - No Grafana, vá até a seção de importação de dashboards
   - Carregue o arquivo de configuração
   - Confirme a importação

5. Após a importação, você poderá visualizar o dashboard com as métricas do projeto.

## GitHub Actions

Para testar o workflow do GitHub Actions, você tem duas opções:

1. Realize um commit em qualquer branch do projeto
2. Ou acesse o repositório no GitHub:
   - Vá até a aba "Actions"
   - Localize o workflow principal
   - Clique em "Run workflow"

O workflow será executado automaticamente, e você poderá acompanhar o progresso na aba Actions.