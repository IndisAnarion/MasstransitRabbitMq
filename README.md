# README

## Rabbit-mq Setup

```powershell
docker pull rabbitmq
docker run -d --hostname rabbitmq-test --name test-rabbitmq -p 15672:15672 -p 5672:5672 -e RABBITMQ_DEFAULT_USER=indis -e RABBITMQ_DEFAULT_PASS=123456 rabbitmq:3-management
```

## Redis

<https://redis.io/docs/getting-started/>

```powershell
sudo apt-add-repository ppa:redislabs/redis
sudo apt-get update
sudo apt-get upgrade
sudo apt-get install redis-server
```

Then start the Redis server like so:

```powershell
docker pull redis
docker run --name test-redis -d -p 6379:6379 redis
docker exec -it test-redis /bin/bash
```

Docker container içerisinde terminal açmak

```powershell
docker exec -it test-redis /bin/bash
```

