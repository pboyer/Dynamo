class system-update {

  exec { 'apt-get update':
    command => 'apt-get update',
  }

}