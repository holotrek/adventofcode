#!/usr/bin/php
<?php
if ($argc != 2 || in_array($argv[1], array('--help', '-help', '-h', '-?'))) {
    echo 'Usage: '.$argv[0].' ./puzzleInput.txt'."\n";
    return null;
} else {
    return $argv[1];
}
?>
