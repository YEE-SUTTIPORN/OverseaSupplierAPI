pipeline {
    agent any

    environment {
        DOTNET_ROOT = "/usr/share/dotnet"
        PATH = "${DOTNET_ROOT}:${env.PATH}"
        PROJECT_PATH = "OverseaSupplierAPI" // ตรงกับโฟลเดอร์ .csproj จริง ๆ
    }

    triggers {
        pollSCM('H/5 * * * *') // เช็ค GitHub ทุก 5 นาที
    }

    stages {
        stage('Checkout') {
            steps {
                git credentialsId: 'github-cred-id',
                    url: 'https://github.com/YEE-SUTTIPORN/OverseaSupplierAPI.git'
            }
        }

        stage('Restore') {
            steps {
                sh "dotnet restore ${PROJECT_PATH}"
            }
        }

        stage('Build') {
            steps {
                sh "dotnet build ${PROJECT_PATH} -c Release --no-restore"
            }
        }

        stage('Publish') {
            steps {
                sh "dotnet publish ${PROJECT_PATH} -c Release -o publish --no-restore"
            }
        }

        stage('Deploy') {
            steps {
                sh '''
                sudo rm -rf /var/www/api/*
                sudo cp -r publish/* /var/www/api/
                sudo systemctl restart my-api-service
                '''
            }
        }
    }
}
