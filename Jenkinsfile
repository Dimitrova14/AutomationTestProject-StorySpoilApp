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
                //continue pipeline even if build fails
                catchError(buildResult: 'SUCCESS', stageResult: 'UNSTABLE') {
                    bat 'dotnet test --logger "trx;LogFileName=TestResults.trx" --verbosity normal'
                }
            }
        }
        stage('Upload Test Results') {
            steps {
                script {

                    withCredentials([string(credentialsId: 'f2294878-313e-4b17-8798-f8b675c65872', variable: 'GITHUB_TOKEN')]) {
                        // Define the TestResults directory
                        def testResultsDir = "${env.WORKSPACE}\\TestResults"

                        // Check if the directory exists
                        if (fileExists(testResultsDir)) {
                            echo "TestResults directory found at: ${testResultsDir}"

                            // Git configuration (configuring user for commit)
                            echo "Configuring Git user details..."
                            bat 'git config user.name "Dimitrova14"'
                            bat 'git config user.email "vanina.dimitrova143@gmail.com"'

                            // Git add, commit, and push using the GITHUB_TOKEN in the push URL
                            echo "Adding TestResults to Git..."
                            bat "git add ${testResultsDir}"

                            echo "Committing TestResults to Git..."
                            bat 'git commit -m "Upload TestResults"'

                            echo "Pushing to GitHub..."
                            bat '''
                                git push https://$GITHUB_TOKEN@github.com/Dimitrova14/AutomationTestProject-StorySpoilApp.git HEAD:refs/heads/main
                            '''
                        } else {
                            echo "TestResults directory does not exist."
                        }
                    }
                }
            }
        }
    }
    post {
        always {
            archiveArtifacts artifacts: '**/TestResults/*.trx', allowEmptyArchive: true
            step([
                $class: 'MSTestPublisher',
                testResultsFile: '**/TestResults/*.trx'
            ])
        }
    }

}
