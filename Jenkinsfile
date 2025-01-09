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
                    // Define the TestResults directory
                    def testResultsDir = "${env.WORKSPACE}\\TestResults"

                    // Check if the TestResults directory exists
                    if (fileExists(testResultsDir)) {
                        echo "TestResults directory found at: ${testResultsDir}"

                        // Git configuration
                        echo "Configuring Git user details..."
                        bat 'git config user.name "Dimitrova14"'
                        bat 'git config user.email "vanina.dimitrova143@gmail.com"'

                        // Debugging: Show current Git status
                        echo "Checking Git status before adding files..."
                        bat "git status"

                        // Add TestResults folder to the Git repository
                        echo "Adding TestResults directory to Git..."
                        bat "git add ${testResultsDir}"

                        // Debugging: Show Git status after adding files
                        echo "Git status after adding files:"
                        bat "git status"

                        // Commit the changes
                        echo "Committing changes to Git.."
                        bat 'git commit -m "Upload TestResults"'

                        // Debugging: Show the last commit details
                        echo "Showing last Git commit..."
                        bat "git log -1"

                        // Push the changes to the repository
                        echo "Pushing changes to the remote repository..."
                        bat "git push origin HEAD:refs/heads/main 2>&1"
                    } else {
                        echo "TestResults folder does not exist at: ${testResultsDir}"
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