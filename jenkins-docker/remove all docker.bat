call docker system prune -f
call docker container prune -f
call docker image prune -a -f
call docker volume prune -f
call docker network prune -f
pause