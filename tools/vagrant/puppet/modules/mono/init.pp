class mono{

  $sysPackages = [ 'mono-complete' ]
  package { $sysPackages:
    ensure => "installed",
    require => Exec['apt-get update'],
  }

}
