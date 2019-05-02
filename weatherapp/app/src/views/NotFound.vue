<template>
  <div class="NotFound">
    <h1>City not found</h1>
    <br>
    <button @click="GoBack()" >Back to safety</button>
  </div>
</template>

<script>

export default{
  name: 'Home',
  methods: {
    GoBack(){
      this.$router.push({name: "home"});
    }
  }
}

</script>