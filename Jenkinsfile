pipeline {
    agent any

    stages {

        environment {
            HELLO = VersionNumber([
                versionNumberString : '${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_ID}',
                projectStartDate : '2017-01-01'
            ])
        }
        
        stage('Build') {
            steps {
                echo VersionNumber([
            versionNumberString : '${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_ID}',
            projectStartDate : '2017-01-01'
        ])
            }
        }
        stage('Test') {
            steps {
                bat 'set'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}