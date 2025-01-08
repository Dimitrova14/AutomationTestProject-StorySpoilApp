pipeline {
    agent any

    stages {
        stage("Checkout the code") {
            //checkout the code
            steps {
                git branch: 'main', url: 'https://github.com/Dimitrova14/AutomationTestProject-StorySpoilApp'
            }
        }
        stage("Set up .NET Core") {
            //install .NET core
            steps {
                bat '''
                curl -l -o dotnet-sdk-8.0.404-win-x64.exe https://download.visualstudio.microsoft.com/download/pr/ba3a1364-27d8-472e-a33b-5ce0937728aa/6f9495e5a587406c85af6f93b1c89295/dotnet-sdk-8.0.404-win-x64.exe
                dotnet-sdk-8.0.404-win-x64.exe /quiet /norestart
                '''
            }
        }
        stage("Install dependencies") {
            //install depenedencies
            steps {
                bat 'dotnet restore StorySpoilAppTests.sln'
            }
        }
        
    }

}