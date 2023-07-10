export default (to, from, next) => {
  var veio = from.path
  var vou = to.path
  if(openRoutes.includes(to.name))  {
      next()
  }
  else if (localStorage.getItem('Token') ) {
    next()
  }else {
    next('/')
  }
}