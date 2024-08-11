pipeline {
    agent any

    environment {
        CONTAINER_NAME = 'dotnet6-container'
        APP_PATH = 'p3ops-demo-app'
        PROJECT_PATH = 'src/Server/Server.csproj'
        PUBLISH_PATH = 'publish'
    }

    stages {

        stage('Delete repo') {
            steps {
                sh 'if [ -d p3ops-demo-app ]; then rm -rf p3ops-demo-app; fi'
            }
        }

        stage('Clone repo') {
            steps {
                sh 'git clone --branch main https://github.com/woutverfaillie/p3ops-demo-app.git'
            }
        }

        stage('Set Git Remote URL') {
            steps {
                dir(APP_PATH) {
                    sh 'git config remote.origin.url https://github.com/woutverfaillie/p3ops-demo-app.git'
                }
            }
        }

        stage('Copy files to dotnet6-container') {
            steps {
                script {
                    sh 'docker cp p3ops-demo-app dotnet6-container:/'
                }
            }
        }

        stage('Restore Dependencies') {
            steps {
                echo 'Restoring .NET dependencies...'
                sh 'docker exec ${CONTAINER_NAME} bash -c "dotnet restore ${PROJECT_PATH}"'
            }
        }

        stage('Build Project') {
            steps {
                echo 'Building .NET project...'
                sh 'docker exec ${CONTAINER_NAME} bash -c "dotnet build ${PROJECT_PATH} -c Release -o /app/build"'
            }
        }

        stage('Publish Application') {
            steps {
                echo 'Publishing .NET application...'
                sh 'docker exec ${CONTAINER_NAME} bash -c "dotnet publish ${PROJECT_PATH} -c Release -o ${PUBLISH_PATH}"'
            }
        }

        stage('Run Application') {
            steps {
                echo 'Running the .NET application...'
                sh 'docker exec ${CONTAINER_NAME} bash -c "cd ${PUBLISH_PATH} && nohup dotnet Server.dll > /dev/null 2>&1 &"'
            }
        }
    }

    post {
        always {
            echo 'Cleaning up...'
            sh 'docker logout || true'
        }
    }
}
