pipeline {
    agent {
        node {
            label 'Windows 10 (amd64)'
            customWorkspace 'C:\ProgramData\Jenkins\.jenkins\workspace'
        } 

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
                git branch: 'main', url: 'https://github.com/Dimitrova14/AutomationTestProject-StorySpoilApp'
            }
        }
        stage("") {
            //build project
        }
        stage("") {
            //install dependencies
        }
        stage("") {
            //run tests and generate test report
        }
        stage("") {
            //upload 
        }
    }

    }
}