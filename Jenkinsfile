pipeline {
    agent any

    environment {
        DOTNET_ROOT = "/usr/share/dotnet"
        PROJECT_PATH = "MyApiProject"   // ปรับตามโครงสร้างจริง
    }

    triggers {
        pollSCM('H/5 * * * *')
    }

    stages {
        stage('Checkout') {
            steps {
                git credentialsId: 'github-cred-id', url: 'https://github.com/yourname/yourrepo.git'
            }
        }

        stage('Build') {
            steps {
                sh "dotnet restore ${PROJECT_PATH}"
                sh "dotnet build ${PROJECT_PATH} -c Release"
            }
        }

        stage('Publish') {
            steps {
                sh "dotnet publish ${PROJECT_PATH} -c Release -o publish"
            }
        }

        stage('Deploy') {
            steps {
                sh '''
                rm -rf /var/www/api/*
                cp -r publish/* /var/www/api/
                systemctl restart my-api-service
                '''
            }
        }
    }
}
