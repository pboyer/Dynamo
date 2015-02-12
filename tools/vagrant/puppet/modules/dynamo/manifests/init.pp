class dynamo {
  exec { 'ls':
    command => 'ls',
	cwd => '/dynamo',
	logoutput => true
  }
}