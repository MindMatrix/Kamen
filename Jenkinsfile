pipeline {
    environment {
        FOO = "BAR"
        VERSION = VersionNumber([versionNumberString : "1.0.${BUILD_ID}", projectStartDate : '2017-01-01'])
    }

    agent { label "master" }

    stages {
        stage("foo") {
            steps {
                script{
                    VERSION = VersionNumber([versionNumberString : "1.0.${BUILD_ID}", projectStartDate : '2017-01-01'])
                }
                sh 'echo "FOO is $FOO"'
                sh 'echo "FOO is $VERSION"'
                echo VersionNumber([versionNumberString : "1.0.${BUILD_ID}", projectStartDate : '2017-01-01'])
            }
        }
    }
}