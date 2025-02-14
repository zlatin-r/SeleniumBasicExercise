pipeline {
    agent any
    stages {
        stage('Restore Dependencies') {
            steps {
                bat 'dotnet restore'
            }
        }
        stage('Build Project') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }
        stage('Run dotnet tests') {
            steps {
                bat 'dotnet test'
            }
        }
    }
}
