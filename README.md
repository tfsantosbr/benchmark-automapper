# benchmark-automapper

## Último teste

| Method           |     Mean |   Error |  StdDev |   Median |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
| ---------------- | -------: | ------: | ------: | -------: | --------: | --------: | -------: | --------: |
| StandardCreate   | 164.7 ms | 3.28 ms | 8.48 ms | 161.3 ms | 5750.0000 | 3000.0000 | 250.0000 |    108 MB |
| AutomapperCreate | 176.5 ms | 3.51 ms | 9.26 ms | 175.8 ms | 5750.0000 | 3000.0000 | 250.0000 |    108 MB |
| StandardGet      | 129.7 ms | 2.57 ms | 2.28 ms | 129.5 ms | 6000.0000 | 3200.0000 | 400.0000 |    108 MB |
| AutomapperGet    | 120.8 ms | 1.99 ms | 3.04 ms | 121.0 ms | 5750.0000 | 3000.0000 | 250.0000 |    108 MB |
