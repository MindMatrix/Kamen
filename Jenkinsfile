pipeline {
    environment {
        FOO = "BAR"
        VERSION = "1.0.${BUILD_ID}"
    }

    agent { label "master" }

    stages {
        stage("foo") {
            steps {        
                echo 'Build version ' + VersionNumber([versionNumberString : "${VERSION}", projectStartDate : '2017-01-01'])
                sh 'echo "FOO is $FOO"'
                sh 'echo "FOO is $VERSION"'
            }
        }
    }
}