pipeline {
    agent any

    environment {
        CONTAINER_NAME = 'dotnet6-container'  // Naam van de bestaande container
        APP_PATH = 'p3ops-demo-app'
        PROJECT_PATH = 'src/Server/Server.csproj'
        PUBLISH_PATH = 'publish'
        
        // Zet de omgevingsvariabelen voor de container
        DOTNET_ConnectionStrings__SqlDatabase = "Server=sql-server-container;Database=SportStore;User=SA;Password=SQLcontainer1;MultipleActiveResultSets=true"
        DOTNET_ENVIRONMENT = 'Production'
    }


    stages {
        stage('Delete REPO') {
            steps {
                sh 'if [ -d p3ops-demo-app ]; then rm -rf p3ops-demo-app; fi'
            }
        }

        stage('Git Checkout') {
            steps {
                sh 'git clone --branch main https://github.com/woutverfaillie/p3ops-demo-app'
            }
        }

        stage('Check Docker Containers') {
            steps {
                script {
                    def dotnetContainerExists = sh(script: 'docker ps -q -f name=dotnet6-container', returnStatus: true) == 0
                    def sqlContainerExists = sh(script: 'docker ps -q -f name=sql-container', returnStatus: true) == 0
                    def jenkinsContainerExists = sh(script: 'docker ps -q -f name=jenkins', returnStatus: true) == 0

                    if (!dotnetContainerExists) {
                        error "dotnet6-container is not running"
                    }
                    if (!sqlContainerExists) {
                        error "sql-container is not running"
                    }
                    if (!jenkinsContainerExists) {
                        error "jenkins container is not running"
                    }
                }
            }
        }

        stage('Copy files to dotnet6-container') {
            steps {
                script {
                    sh 'docker cp p3ops-demo-app dotnet6-container:'
                }
            }
        }

        stage('Restore Dependencies') {
            steps {
                echo 'Restoring .NET dependencies...'
                sh 'docker exec ${CONTAINER_NAME} bash -c "dotnet restore ${PROJECT_PATH}"'
            }
        }

        stage('Build in dotnet6-container') {
            steps {
                echo 'Building .NET project...'
                sh 'docker exec ${CONTAINER_NAME} bash -c "dotnet build ${PROJECT_PATH} -c Release -o /app/build"'
            }
        }

        stage('Publish and start app') {
            steps {
                echo 'Publishing .NET application...'
                sh 'docker exec ${CONTAINER_NAME} bash -c "dotnet publish ${PROJECT_PATH} -c Release -o ${PUBLISH_PATH}"'
            }

        }
     stage('Run Application') {
            steps {
        echo 'Running the .NET application...'
        sh """
            docker exec ${CONTAINER_NAME} bash -c '
            export DOTNET_ConnectionStrings__SqlDatabase="${DOTNET_ConnectionStrings__SqlDatabase}" &&
            export DOTNET_ENVIRONMENT="${DOTNET_ENVIRONMENT}" &&
            cd ${PUBLISH_PATH} &&
            nohup dotnet Server.dll > /dev/null 2>&1 &
            '
        """
            }
        }
    }

    post {
        always {
            echo 'Cleaning up...'
            sh 'docker logout'
        }
     }
}