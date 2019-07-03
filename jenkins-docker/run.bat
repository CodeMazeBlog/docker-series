call docker build .\master -t jenkins-master
call docker build .\slave -t jenkins-slave
	
docker-compose -f ..\docker-compose.ci.yml up

pause