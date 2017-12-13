#!/usr/bin/php
<?php
    $fileName = require('get-args.php');
    if ($fileName) {
        $lines = file_get_contents($fileName);
        $lines = array_filter(explode(PHP_EOL, $lines), 'strlen');
        $idx = 0;
        $jumpCount = 0;
        while ($idx > -1 && $idx < count($lines)) {
            $jump = jump($lines, $idx);
            $idx += $jump;
            $jumpCount++;
        }
        echo $jumpCount."\n";
    }

    function jump(&$arr, $idx) {
        $jump = $arr[$idx];
        $arr[$idx] = $arr[$idx] + 1;
        return $jump;
    }
?>

