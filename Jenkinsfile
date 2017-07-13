pipeline {
    environment {
        FOO = VERSION = VersionNumber([versionNumberString : '1.0.${BUILD_ID}', projectStartDate : '2017-01-01'])
    }

    agent { label "master" }

    stages {
        stage("foo") {
            steps {
                sh 'echo "FOO is $FOO"'
            }
        }
    }
}