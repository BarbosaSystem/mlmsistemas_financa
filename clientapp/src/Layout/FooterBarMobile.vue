<template>
    <nav style="z-index: 1000" class="navbar bottom navbar-expand-lg bg-gray d-block d-md-none d-lg-none">
      <div class="container">
        <ul class="navbar-nav nav-fill w-100">
          <router-link to="/principal" tag="li" class="nav-item" exact-active-class="ativo">
            <i class="nav-link fas fa-home"></i>
           <p class="nav-text text-center">Home</p>
          </router-link>
          <router-link to="/periodo" tag="li" class="nav-item" exact-active-class="ativo">
            <i class="nav-link fas fa-calendar-alt"></i>
            <p class="nav-text text-center">Periodo</p>
          </router-link>
          <router-link to="/movimentos" tag="li" class="nav-item" exact-active-class="ativo">
            <i class="nav-link fas fa-money-bill-wave"></i>
            <p class="nav-text text-center">Movimentos</p>
          </router-link>
          <router-link to="/settings" tag="li" class="nav-item" exact-active-class="ativo">
            <i class="nav-link fas fa-cogs"></i>
            <p class="nav-text text-center">Config.</p>
          </router-link>
          <!-- <router-link to="/logout" tag="li" class="nav-item" exact-active-class="ativo">
            <i class="nav-link fas fa-power-off"></i> <p class="nav-text text-center">Sair</p>
          </router-link> -->
        </ul>
      </div>
    </nav>
</template>
<script>
export default {
    
}
</script>