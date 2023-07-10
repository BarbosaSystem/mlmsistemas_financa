<template>
  <nav aria-label="Page navigation example" v-if="listaDados.length > 0">
    <ul class="pagination justify-content-center">
      <!--     ANTERIOR    -->

      <template>
        <div>
          <li class="page-item disabled" v-if="anterior">
            <a class="page-link" href="#">Anterior</a>
          </li>

          <li class="page-item" @click="PreviousPage()" v-else>
            <a class="page-link" href="#">Anterior</a>
          </li>
        </div>
      </template>

      <!--     ANTERIOR    -->
      <template>
        <li
          class="page-item"
          v-if="(pagination.currentPage != 0)"
          @click="ItemAtual(pagination.currentPage - 1)"
        >
          <a href="#" class="page-link">{{pagination.currentPage}}</a>
        </li>
      </template>
      <li class="page-item active">
        <a href="#" class="page-link">{{pagination.currentPage + 1}}</a>
      </li>
      <template>
        <li
          class="page-item"
          v-if="!(pagination.currentPage == pagination.totalPages - 1)"
          @click="ItemAtual(pagination.currentPage + 1)"
        >
          <a href="#" class="page-link">{{pagination.currentPage + 2}}</a>
        </li>
      </template>

      <template>
        <div>
          <li class="page-item disabled" v-if="ultimo">
            <a class="page-link" href="#">Próximo</a>
          </li>
          <li class="page-item" @click="NextPage()" v-else>
            <a class="page-link" href="#">Próximo</a>
          </li>
        </div>
      </template>
    </ul>
  </nav>
</template>
<script>
export default {
    props: ['listaDados'],
    data() {
        return {
            anterior: 0,
            ultimo: 0,
            pagination: {
                totalPages: 0,
                currentPage: 0,
                itensPage: 5
            }
        }
    },
    methods: {
        ItemAtual(codigo) {
            this.pagination.currentPage = codigo
        },
        NextPage() {
            if (this.pagination.currentPage < this.pagination.totalPages) {
                this.pagination.currentPage += 1
            }
        },
        PreviousPage() {
            if (this.pagination.currentPage > 0) {
                this.pagination.currentPage -= 1
            }
        },
    }
}
</script>