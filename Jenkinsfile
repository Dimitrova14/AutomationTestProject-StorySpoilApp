pipeline {
    agent any

    stages {
        stage("Checkout the code") {
            //checkout the code
            steps {
                git branch: 'main', url: 'https://github.com/Dimitrova14/AutomationTestProject-StorySpoilApp'
            }
        }
        stage('Debug Workspace') {
            steps {
                bat """
                echo Workspace Directory:
                echo %WORKSPACE%
                dir /s %WORKSPACE%
                """
            }
        }
        stage("Set up .NET Core") {
            //install .NET core
            steps {
                bat '''
                curl --ssl-no-revoke -l -o dotnet-sdk-8.0.111-win-x64.exe https://download.visualstudio.microsoft.com/download/pr/25556d50-c52f-4941-b5d8-fa0e7e0168c2/aa802988431129dbed20d2d77d5d49bf/dotnet-sdk-8.0.111-win-x64.exe
                dotnet-sdk-8.0.111-win-x64.exe /quiet /norestart
                '''
            }
        }
        stage("Install dependencies") {
            //install depenedencies
            steps {
                bat 'dotnet restore StorySpoilAppTests.sln'
            }
        }
        stage("Run Tests and Generate Test Report") {
            //install depenedencies
            steps {
                bat 'dotnet test --logger "trx;LogFileName=TestResults.trx" --verbosity normal'
            }
        }
        
    }

    post {
        always {
            archiveArtificats artificats: '**/TestResults/*.trx', allowEmptyArchive: true
            step([
                $class: 'MSTestPublisher',
                testResultsFile: '**/TestResults/*.trx'
            ])
        }
    }

}