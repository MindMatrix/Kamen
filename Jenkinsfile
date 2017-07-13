pipeline {
    environment {
        FOO = "BAR"
        VERSION = "1.0.${BUILD_ID}"
    }

    agent { label "master" }

    stages {
        stage("foo") {
            steps {
                sh 'echo "FOO is $FOO"'
                sh 'echo "FOO is $VERSION"'
            }
        }
    }
}