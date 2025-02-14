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
                bat 'dotnet test TestProject1/TestProject1.csproj --no-build --verbosity normal'
            }
            steps {
                  bat 'dotnet test TestProject2/TestProject2.csproj --no-build --verbosity normal'
              }
            steps {
                  bat 'dotnet test TestProject3/TestProject3.csproj --no-build --verbosity normal'
              }
        }
    }
}
