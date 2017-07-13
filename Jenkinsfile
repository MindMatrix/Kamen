pipeline {
    agent any
    environment {
        VERSION = VersionNumber([versionNumberString : '1.0.${BUILD_ID}', projectStartDate : '2017-01-01'])
    }
    stages {
        stage('Build') {
            steps {
                sh 'echo %VERSION%'
                sh 'echo $env.VERSION'
                sh 'echo ${VERSION}'
                sh 'echo $(VERSION)'
                sh 'echo $(VERSION}'
                sh 'echo $(env.VERSION)'
                sh 'echo ${env.VERSION}'
                sh 'echo $(env.VERSION}'
            }
        }
        stage('Test') {
            steps {
                sh 'set'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}
