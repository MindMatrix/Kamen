pipeline {
    agent any
    environment {
        VERSION = VersionNumber([
            versionNumberString : '${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_ID}',
            projectStartDate : '2017-01-01'
        ]);
    }

    stages {
        stage('Build') {
            steps {
                bat 'echo %VERSION%'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}